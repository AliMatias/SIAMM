using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{
    //prefab asignado por interfaz
    [SerializeField]
    private Atom atomPrefab;
    //lista que maneja los átomos en pantalla
    private List<Atom> atomsList = new List<Atom>();
    //Lista de posiciones ocupadas/libres
    private List<Vector3> planePositions = new List<Vector3>();
    private List<bool> availablePositions = new List<bool>();
    //indica el átomo seleccionad. (-1 -> ninguno)
    private int selectedAtom = -1;

    private void Awake()
    {
        LoadPositions();
    }

    //agregar nuevo átomo al espacio de trabajo
    public void NewAtom()
    {
        //intento obtener una posición disponible random
        Vector3 position;
        try
        {
            position = ObtainRandomPosition();
        }
        //si no hay mas posiciones disponibles, lo loggeo y me voy
        catch(NoPositionsLeftException nple)
        {
            Debug.Log(nple.Message);
            return;
        }
        //instancio
        Atom spawnedAtom = Instantiate<Atom>(atomPrefab);
        //asigno random position
        spawnedAtom.transform.localPosition = position;
        //agrego a la lista
        atomsList.Add(spawnedAtom);
        //asigno su índice a este átomo
        spawnedAtom.AtomIndex = atomsList.Count - 1;
        //spawneo un protón
        spawnedAtom.SpawnNucleon(true);
    }

    //seteo las posibles posiciones, por ahora hardcodeadas
    private void LoadPositions()
    {
        planePositions.Add(new Vector3(-2.8f, 3, 0));
        planePositions.Add(new Vector3(0, 3, 0));
        planePositions.Add(new Vector3(2.8f, 3, 0));
        planePositions.Add(new Vector3(-2.8f, 1, 0));
        planePositions.Add(new Vector3(0, 1, 0));
        planePositions.Add(new Vector3(2.8f, 1, 0));
        //todas están disponibles al principio
        foreach (Vector3 position in planePositions)
        {
            availablePositions.Add(true);
        }
        //menos la primera, que es la que ocupa el átomo por defecto.
        //esto se debe borrar cuando este átomo no este al principio
        availablePositions[0] = false;
    } 

    //obtengo una posición random en el plano
    private Vector3 ObtainRandomPosition()
    {
        //si no hay mas disponibles tiro exception
        if (NoPositionsLeft())
            throw (new NoPositionsLeftException("No hay más posiciones disponibles"));
        int positions = availablePositions.Count;
        while (true)
        {
            //obtengo posición random hasta encontrar una libre
            int randomIndex = Random.Range(0, positions);
            if (availablePositions[randomIndex])
            {
                availablePositions[randomIndex] = false;
                return planePositions[randomIndex];
            }
        }

    }

    //chequeo si hay posiciones disponibles
    private bool NoPositionsLeft()
    {
        foreach(bool available in availablePositions)
        {
            if (available)
                return false;
        }
        return true;
    }

    //seleccionar átomo
    public void SelectAtom(int index)
    {
        //verifico si este átomo estaba seleccionado
        if(index == selectedAtom)
        {
            Debug.Log("Este átomo ya estaba seleccionado");
            return;
        }
        //si habia uno seleccionado, lo des-selecciono
        if(selectedAtom != -1)
            DeselectParticlesFromAtom(selectedAtom);
        //selecciono el nuevo
        SelectParticlesFromAtom(index);
        //y obtengo su índice
        selectedAtom = index;
    }

    //para el brillo que indica selección en todas las partículas de este átomo
    private void DeselectParticlesFromAtom(int index)
    {
        Atom atom = atomsList[index];
        Queue<GameObject> queue = atom.ProtonQueue;
        StopAllHighlights(queue);
        queue = atom.NeutronQueue;
        StopAllHighlights(queue);
        queue = atom.ElectronQueue;
        StopAllHighlights(queue);
    }

    //quita la ilumnación a todas las particulas de esta cola
    private void StopAllHighlights(Queue<GameObject> queue){
        foreach (GameObject obj in queue)
        {
            obj.GetComponent<HighlightObject>().StopHighlight();
        }
    }

    //asigna el brillo que indica selección a todas las partículas de este átomo
    private void SelectParticlesFromAtom(int index)
    {
        Atom atom = atomsList[index];
        Queue<GameObject> queue = atom.ProtonQueue;
        StartAllHighlights(queue);
        queue = atom.NeutronQueue;
        StartAllHighlights(queue);
        queue = atom.ElectronQueue;
        StartAllHighlights(queue);
    }

    //ilumina las partículas de esta cola
    private void StartAllHighlights(Queue<GameObject> queue){
        foreach(GameObject obj in queue)
        {
            obj.GetComponent<HighlightObject>().StartHighlight();
        }
    }

    //agregar la partícula indicada al átomo seleccionado
    public void AddParticleToSelectedAtom(int particle){
        if(selectedAtom==-1){
            Debug.Log("No hay ningún átomo seleccionado");
            return;
        }
        //agarro el átomo indicado de la lista
        Atom atom = atomsList[selectedAtom];
        if(particle==0){
            atom.SpawnNucleon(true);
        }else if (particle==1){
            atom.SpawnNucleon(false);
        }else if(particle ==2){
            atom.SpawnElectron();
        }else{
            Debug.Log("Se ingreso un índice de partícula equivocado.");
            Debug.Log("Los valores correctos son: 0-protón, 1-neutrón, 2-electrón");
            return;
        }
    }

    //quitar del átomo seleccionado la partícula indicada
    public void RemoveParticleFromSelectedAtom(int particle){
        if(selectedAtom==-1){
            Debug.Log("No hay ningún átomo seleccionado");
            return;
        }
        //agarro el átomo de la lista
        Atom atom = atomsList[selectedAtom];
        if(particle==0){
            atom.RemoveProton();
        }else if (particle==1){
            atom.RemoveNeutron();
        }else if(particle ==2){
            atom.RemoveElectron();
        }else{
            Debug.Log("Se ingreso un índice de partícula equivocado.");
            Debug.Log("Los valores correctos son: 0-protón, 1-neutrón, 2-electrón");
            return;
        }
    }

}
