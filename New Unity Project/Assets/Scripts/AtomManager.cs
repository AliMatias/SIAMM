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
        //spawneo un protón
        spawnedAtom.SpawnNucleon(true);
    }

    //seteo las posibles posiciones, por ahora hardcodeadas
    private void LoadPositions()
    {
        planePositions.Add(new Vector3(-2.5f, 3, 0));
        planePositions.Add(new Vector3(0, 3, 0));
        planePositions.Add(new Vector3(2.5f, 3, 0));
        planePositions.Add(new Vector3(-2.5f, 1, 0));
        planePositions.Add(new Vector3(0, 1, 0));
        planePositions.Add(new Vector3(2.5f, 1, 0));
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

}
