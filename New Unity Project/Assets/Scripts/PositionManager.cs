using System.Collections.Generic;
using UnityEngine;

// Clase singleton para manejar las posiciones en el workspace
public class PositionManager
{
    public List<Vector3> PlanePositions { get; set; }
    public List<bool> AvailablePositions { get; set; }

    private static PositionManager instance = null;

    private PositionManager()
    {
        PlanePositions = new List<Vector3>();
        AvailablePositions = new List<bool>();
        this.LoadPositions();
    }

    public static PositionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PositionManager();
            }
            return instance;
        }
    }

    //seteo las posibles posiciones, por ahora hardcodeadas
    private void LoadPositions()
    {
        PlanePositions.Add(new Vector3(-2, 3, 0));
        PlanePositions.Add(new Vector3(2, 3, 0));
        PlanePositions.Add(new Vector3(-2, 1, 0));
        PlanePositions.Add(new Vector3(2, 1, 0));
        //todas están disponibles al principio
        foreach (Vector3 position in PlanePositions)
        {
            AvailablePositions.Add(true);
        }
    }

    //obtengo una posición random en el plano
    public int ObtainRandomPositionIndex()
    {
        //si no hay mas disponibles tiro exception
        if (NoPositionsLeft())
            throw (new NoPositionsLeftException("No hay más posiciones disponibles"));
        int positions = AvailablePositions.Count;
        while (true)
        {
            //obtengo posición random hasta encontrar una libre
            int randomIndex = Random.Range(0, positions);
            if (AvailablePositions[randomIndex])
            {
                AvailablePositions[randomIndex] = false;
                return randomIndex;
            }
        }
    }

    //chequeo si hay posiciones disponibles
    private bool NoPositionsLeft()
    {
        foreach (bool available in AvailablePositions)
        {
            if (available)
                return false;
        }
        return true;
    }
}
