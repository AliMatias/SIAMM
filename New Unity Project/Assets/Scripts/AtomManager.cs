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
    //indica el último átomo seleccionado. (-1 -> ninguno)
    private int lastSelectedAtom = -1;
    //booleano que indica si el modo combinación está activo
    private bool combineMode = false;
    //lista que indica elementos seleccionados en modo combinación
    private List<int> selectedAtoms = new List<int>();
    private PositionManager positionManager = PositionManager.Instance;

    //esto està porque lo necesita el "combination manager"
    //seguro cuando busque la combinaciòn posta, va a necesitar otra cosa, no esto.
    public List<int> SelectedAtoms { get => selectedAtoms; set => selectedAtoms = value; }

    //cambia entre modo combinación o normal
    public void SwitchCombineMode(){
        combineMode = !combineMode;
        if(!combineMode){
            //si estoy saliendo del modo combinación quito las selecciones de todos
            foreach (int index in selectedAtoms){
                DeselectParticlesFromAtom(index);
            }
            selectedAtoms = new List<int>();
            lastSelectedAtom = -1;
        }else{
            //si entré al modo combinación, necesito agregar el seleccionado a la lista (si hay uno)
            if(lastSelectedAtom!=-1){
                selectedAtoms.Add(lastSelectedAtom);
            }
        }
    }


    //agregar nuevo átomo al espacio de trabajo
    public void NewAtom(bool withProton)
    {
        //intento obtener una posición disponible random
        int position;
        try
        {
            position = positionManager.ObtainRandomPositionIndex();
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
        spawnedAtom.transform.localPosition = positionManager.PlanePositions[position];
        //agrego a la lista
        atomsList.Add(spawnedAtom);
        //asigno su índice a este átomo
        spawnedAtom.AtomIndex = position;
        //si dice que lo spawnee con un proton, lo hago
        if(withProton){
            spawnedAtom.SpawnNucleon(true, false);
        }
    }

    //seleccionar átomo
    public void SelectAtom(int index)
    {
        //verifico si este átomo estaba seleccionado
        //ya sea si esta en la lista o si fue el último seleccionado
        if(selectedAtoms.IndexOf(index) != -1 || lastSelectedAtom == index)
        {
            Debug.Log("Este átomo ya estaba seleccionado. Se quitará la selección");
            DeselectParticlesFromAtom(index);
            lastSelectedAtom = -1;
            if(combineMode){
                selectedAtoms.Remove(index);
            }
            return;
        }
        //si habia uno seleccionado, lo des-selecciono
        if(lastSelectedAtom != -1 && !combineMode){
            DeselectParticlesFromAtom(lastSelectedAtom);
        }
        //selecciono el nuevo
        SelectParticlesFromAtom(index);
        //y obtengo su índice
        lastSelectedAtom = index;
        if(combineMode){
            selectedAtoms.Add(index);
        }
    }

    //ya que el índice del átomo depende de la posición, 
    //necesito recorrer la lista para encontrarlo
    public Atom FindAtomInList(int index){
        foreach(Atom atom in atomsList){
            if(atom.AtomIndex == index){
                return atom;
            }
        }
        return null;
    }

    //quita el brillo al átomo seleccionado
    private void DeselectParticlesFromAtom(int index)
    {
        Atom atom = FindAtomInList(index);
        atom.Deselect();
    }

    //comienza el brillo en el átomo seleccionado
    private void SelectParticlesFromAtom(int index)
    {
        Atom atom = FindAtomInList(index);
        atom.Select();
    }

    //agregar la partícula indicada al átomo seleccionado
    public void AddParticleToSelectedAtom(int particle){
        if(lastSelectedAtom==-1){
            Debug.Log("No hay ningún átomo seleccionado");
            return;
        }
        //agarro el átomo indicado de la lista
        Atom atom = FindAtomInList(lastSelectedAtom);
        if(particle==0){
            atom.SpawnNucleon(true, false);
        }else if (particle==1){
            atom.SpawnNucleon(false, false);
        }else if(particle ==2){
            atom.SpawnElectron(false);
        }else{
            Debug.Log("Se ingreso un índice de partícula equivocado.");
            Debug.Log("Los valores correctos son: 0-protón, 1-neutrón, 2-electrón");
            return;
        }
    }

    //quitar del átomo seleccionado la partícula indicada
    public void RemoveParticleFromSelectedAtom(int particle){
        if(lastSelectedAtom==-1){
            Debug.Log("No hay ningún átomo seleccionado");
            return;
        }
        //agarro el átomo de la lista
        Atom atom = FindAtomInList(lastSelectedAtom);
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

    //BORRAR átomo seleccionado.
    public void DeleteSelectedAtom(){
        if(lastSelectedAtom==-1){
            Debug.Log("No hay ningún átomo seleccionado");
            return;
        }
        DeleteAtom(lastSelectedAtom);
        //ahora no hay átomo seleccionado
        lastSelectedAtom = -1;
    }

    //BORRAR átomo
    public void DeleteAtom(int index)
    {
        //primero lo encuentro
        Atom atom = FindAtomInList(index);
        //lo saco de la lista
        atomsList.Remove(atom);
        //lo destruyo
        Destroy(atom);
        //disponibilizo la posición denuevo
        positionManager.AvailablePositions[index] = true;
        Debug.Log("Se ha borrado el átomo " + index);
    }

    //spawnear átomo seleccionado en la tabla periódica
    public void SpawnFromPeriodicTable(string elementName){
        int oldAtomsCount = atomsList.Count;
        NewAtom(false);
        int newAtomsCount = atomsList.Count;
        if(oldAtomsCount < newAtomsCount){
            Atom newAtom = atomsList[newAtomsCount-1];
            newAtom.SpawnFromPeriodicTable(elementName);
        }else{
            Debug.Log("No hay más lugar para crear un nuevo átomo.");
        }
    }

}
