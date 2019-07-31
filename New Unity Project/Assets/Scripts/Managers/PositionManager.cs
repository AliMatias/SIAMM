using System.Collections.Generic;
using UnityEngine;

// Clase singleton para manejar las posiciones en el workspace
public class PositionManager
{
    public List<Vector3> Positions { get; set; }
    public List<bool> AvailablePositions { get; set; }

    private static PositionManager instance = null;

    private PositionManager()
    {
        Positions = new List<Vector3>();
        AvailablePositions = new List<bool>();
        LoadPositions();
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

#region positions
    //seteo las posibles posiciones, por ahora hardcodeadas
    private void LoadPositions()
    {
        //plano central
        Positions.Add(new Vector3(-2.5f, 1, 0));
        Positions.Add(new Vector3(2.5f, 1, 0));
        Positions.Add(new Vector3(-2.5f, 3, 0));
        Positions.Add(new Vector3(2.5f, 3, 0));
        Positions.Add(new Vector3(2.5f, 5, 0));
        Positions.Add(new Vector3(-2.5f, 5, 0));
        Positions.Add(new Vector3(2.5f, 7, 0));
        Positions.Add(new Vector3(-2.5f, 7, 0));
        Positions.Add(new Vector3(-7.5f, 1, 0));
        Positions.Add(new Vector3(7.5f, 1, 0));
        Positions.Add(new Vector3(-7.5f, 3, 0));
        Positions.Add(new Vector3(7.5f, 3, 0));
        Positions.Add(new Vector3(7.5f, 5, 0));
        Positions.Add(new Vector3(-7.5f, 5, 0));
        Positions.Add(new Vector3(7.5f, 7, 0));
        Positions.Add(new Vector3(-7.5f, 7, 0));
        Positions.Add(new Vector3(-12.5f, 1, 0));
        Positions.Add(new Vector3(12.5f, 1, 0));
        Positions.Add(new Vector3(-12.5f, 3, 0));
        Positions.Add(new Vector3(12.5f, 3, 0));
        Positions.Add(new Vector3(12.5f, 5, 0));
        Positions.Add(new Vector3(-12.5f, 5, 0));
        Positions.Add(new Vector3(12.5f, 7, 0));
        Positions.Add(new Vector3(-12.5f, 7, 0));

        //otros planos (solo cambian en Z)
        //Z=4
        Positions.Add(new Vector3(-2.5f, 1, 4));
        Positions.Add(new Vector3(2.5f, 1, 4));
        Positions.Add(new Vector3(-2.5f, 3, 4));
        Positions.Add(new Vector3(2.5f, 3, 4));
        Positions.Add(new Vector3(2.5f, 5, 4));
        Positions.Add(new Vector3(-2.5f, 5, 4));
        Positions.Add(new Vector3(2.5f, 7, 4));
        Positions.Add(new Vector3(-2.5f, 7, 4));
        Positions.Add(new Vector3(-7.5f, 1, 4));
        Positions.Add(new Vector3(7.5f, 1, 4));
        Positions.Add(new Vector3(-7.5f, 3, 4));
        Positions.Add(new Vector3(7.5f, 3, 4));
        Positions.Add(new Vector3(7.5f, 5, 4));
        Positions.Add(new Vector3(-7.5f, 5, 4));
        Positions.Add(new Vector3(7.5f, 7, 4));
        Positions.Add(new Vector3(-7.5f, 7, 4));
        Positions.Add(new Vector3(-12.5f, 1, 4));
        Positions.Add(new Vector3(12.5f, 1, 4));
        Positions.Add(new Vector3(-12.5f, 3, 4));
        Positions.Add(new Vector3(12.5f, 3, 4));
        Positions.Add(new Vector3(12.5f, 5, 4));
        Positions.Add(new Vector3(-12.5f, 5, 4));
        Positions.Add(new Vector3(12.5f, 7, 4));
        Positions.Add(new Vector3(-12.5f, 7, 4));
        //Z=8
        Positions.Add(new Vector3(-2.5f, 1, 8));
        Positions.Add(new Vector3(2.5f, 1, 8));
        Positions.Add(new Vector3(-2.5f, 3, 8));
        Positions.Add(new Vector3(2.5f, 3, 8));
        Positions.Add(new Vector3(2.5f, 5, 8));
        Positions.Add(new Vector3(-2.5f, 5, 8));
        Positions.Add(new Vector3(2.5f, 7, 8));
        Positions.Add(new Vector3(-2.5f, 7, 8));
        Positions.Add(new Vector3(-7.5f, 1, 8));
        Positions.Add(new Vector3(7.5f, 1, 8));
        Positions.Add(new Vector3(-7.5f, 3, 8));
        Positions.Add(new Vector3(7.5f, 3, 8));
        Positions.Add(new Vector3(7.5f, 5, 8));
        Positions.Add(new Vector3(-7.5f, 5, 8));
        Positions.Add(new Vector3(7.5f, 7, 8));
        Positions.Add(new Vector3(-7.5f, 7, 8));
        Positions.Add(new Vector3(-12.5f, 1, 8));
        Positions.Add(new Vector3(12.5f, 1, 8));
        Positions.Add(new Vector3(-12.5f, 3, 8));
        Positions.Add(new Vector3(12.5f, 3, 8));
        Positions.Add(new Vector3(12.5f, 5, 8));
        Positions.Add(new Vector3(-12.5f, 5, 8));
        Positions.Add(new Vector3(12.5f, 7, 8));
        Positions.Add(new Vector3(-12.5f, 7, 8));
        //Z=12
        Positions.Add(new Vector3(-2.5f, 1, 12));
        Positions.Add(new Vector3(2.5f, 1, 12));
        Positions.Add(new Vector3(-2.5f, 3, 12));
        Positions.Add(new Vector3(2.5f, 3, 12));
        Positions.Add(new Vector3(2.5f, 5, 12));
        Positions.Add(new Vector3(-2.5f, 5, 12));
        Positions.Add(new Vector3(2.5f, 7, 12));
        Positions.Add(new Vector3(-2.5f, 7, 12));
        Positions.Add(new Vector3(-7.5f, 1, 12));
        Positions.Add(new Vector3(7.5f, 1, 12));
        Positions.Add(new Vector3(-7.5f, 3, 12));
        Positions.Add(new Vector3(7.5f, 3, 12));
        Positions.Add(new Vector3(7.5f, 5, 12));
        Positions.Add(new Vector3(-7.5f, 5, 12));
        Positions.Add(new Vector3(7.5f, 7, 12));
        Positions.Add(new Vector3(-7.5f, 7, 12));
        Positions.Add(new Vector3(-12.5f, 1, 12));
        Positions.Add(new Vector3(12.5f, 1, 12));
        Positions.Add(new Vector3(-12.5f, 3, 12));
        Positions.Add(new Vector3(12.5f, 3, 12));
        Positions.Add(new Vector3(12.5f, 5, 12));
        Positions.Add(new Vector3(-12.5f, 5, 12));
        Positions.Add(new Vector3(12.5f, 7, 12));
        Positions.Add(new Vector3(-12.5f, 7, 12));
        //Z=16
        Positions.Add(new Vector3(-2.5f, 1, 16));
        Positions.Add(new Vector3(2.5f, 1, 16));
        Positions.Add(new Vector3(-2.5f, 3, 16));
        Positions.Add(new Vector3(2.5f, 3, 16));
        Positions.Add(new Vector3(2.5f, 5, 16));
        Positions.Add(new Vector3(-2.5f, 5, 16));
        Positions.Add(new Vector3(2.5f, 7, 16));
        Positions.Add(new Vector3(-2.5f, 7, 16));
        Positions.Add(new Vector3(-7.5f, 1, 16));
        Positions.Add(new Vector3(7.5f, 1, 16));
        Positions.Add(new Vector3(-7.5f, 3, 16));
        Positions.Add(new Vector3(7.5f, 3, 16));
        Positions.Add(new Vector3(7.5f, 5, 16));
        Positions.Add(new Vector3(-7.5f, 5, 16));
        Positions.Add(new Vector3(7.5f, 7, 16));
        Positions.Add(new Vector3(-7.5f, 7, 16));
        Positions.Add(new Vector3(-12.5f, 1, 16));
        Positions.Add(new Vector3(12.5f, 1, 16));
        Positions.Add(new Vector3(-12.5f, 3, 16));
        Positions.Add(new Vector3(12.5f, 3, 16));
        Positions.Add(new Vector3(12.5f, 5, 16));
        Positions.Add(new Vector3(-12.5f, 5, 16));
        Positions.Add(new Vector3(12.5f, 7, 16));
        Positions.Add(new Vector3(-12.5f, 7, 16));
        //Z=-4
        Positions.Add(new Vector3(-2.5f, 1, -4));
        Positions.Add(new Vector3(2.5f, 1, -4));
        Positions.Add(new Vector3(-2.5f, 3, -4));
        Positions.Add(new Vector3(2.5f, 3, -4));
        Positions.Add(new Vector3(2.5f, 5, -4));
        Positions.Add(new Vector3(-2.5f, 5, -4));
        Positions.Add(new Vector3(2.5f, 7, -4));
        Positions.Add(new Vector3(-2.5f, 7, -4));
        Positions.Add(new Vector3(-7.5f, 1, -4));
        Positions.Add(new Vector3(7.5f, 1, -4));
        Positions.Add(new Vector3(-7.5f, 3, -4));
        Positions.Add(new Vector3(7.5f, 3, -4));
        Positions.Add(new Vector3(7.5f, 5, -4));
        Positions.Add(new Vector3(-7.5f, 5, -4));
        Positions.Add(new Vector3(7.5f, 7, -4));
        Positions.Add(new Vector3(-7.5f, 7, -4));
        Positions.Add(new Vector3(-12.5f, 1, -4));
        Positions.Add(new Vector3(12.5f, 1, -4));
        Positions.Add(new Vector3(-12.5f, 3, -4));
        Positions.Add(new Vector3(12.5f, 3, -4));
        Positions.Add(new Vector3(12.5f, 5, -4));
        Positions.Add(new Vector3(-12.5f, 5, -4));
        Positions.Add(new Vector3(12.5f, 7, -4));
        Positions.Add(new Vector3(-12.5f, 7, -4));
        //Z=-8
        Positions.Add(new Vector3(-2.5f, 1, -8));
        Positions.Add(new Vector3(2.5f, 1, -8));
        Positions.Add(new Vector3(-2.5f, 3, -8));
        Positions.Add(new Vector3(2.5f, 3, -8));
        Positions.Add(new Vector3(2.5f, 5, -8));
        Positions.Add(new Vector3(-2.5f, 5, -8));
        Positions.Add(new Vector3(2.5f, 7, -8));
        Positions.Add(new Vector3(-2.5f, 7, -8));
        Positions.Add(new Vector3(-7.5f, 1, -8));
        Positions.Add(new Vector3(7.5f, 1, -8));
        Positions.Add(new Vector3(-7.5f, 3, -8));
        Positions.Add(new Vector3(7.5f, 3, -8));
        Positions.Add(new Vector3(7.5f, 5, -8));
        Positions.Add(new Vector3(-7.5f, 5, -8));
        Positions.Add(new Vector3(7.5f, 7, -8));
        Positions.Add(new Vector3(-7.5f, 7, -8));
        Positions.Add(new Vector3(-12.5f, 1, -8));
        Positions.Add(new Vector3(12.5f, 1, -8));
        Positions.Add(new Vector3(-12.5f, 3, -8));
        Positions.Add(new Vector3(12.5f, 3, -8));
        Positions.Add(new Vector3(12.5f, 5, -8));
        Positions.Add(new Vector3(-12.5f, 5, -8));
        Positions.Add(new Vector3(12.5f, 7, -8));
        Positions.Add(new Vector3(-12.5f, 7, -8));
        //Z=-12
        Positions.Add(new Vector3(-2.5f, 1, -12));
        Positions.Add(new Vector3(2.5f, 1, -12));
        Positions.Add(new Vector3(-2.5f, 3, -12));
        Positions.Add(new Vector3(2.5f, 3, -12));
        Positions.Add(new Vector3(2.5f, 5, -12));
        Positions.Add(new Vector3(-2.5f, 5, -12));
        Positions.Add(new Vector3(2.5f, 7, -12));
        Positions.Add(new Vector3(-2.5f, 7, -12));
        Positions.Add(new Vector3(-7.5f, 1, -12));
        Positions.Add(new Vector3(7.5f, 1, -12));
        Positions.Add(new Vector3(-7.5f, 3, -12));
        Positions.Add(new Vector3(7.5f, 3, -12));
        Positions.Add(new Vector3(7.5f, 5, -12));
        Positions.Add(new Vector3(-7.5f, 5, -12));
        Positions.Add(new Vector3(7.5f, 7, -12));
        Positions.Add(new Vector3(-7.5f, 7, -12));
        Positions.Add(new Vector3(-12.5f, 1, -12));
        Positions.Add(new Vector3(12.5f, 1, -12));
        Positions.Add(new Vector3(-12.5f, 3, -12));
        Positions.Add(new Vector3(12.5f, 3, -12));
        Positions.Add(new Vector3(12.5f, 5, -12));
        Positions.Add(new Vector3(-12.5f, 5, -12));
        Positions.Add(new Vector3(12.5f, 7, -12));
        Positions.Add(new Vector3(-12.5f, 7, -12));
        //Z=-16
        Positions.Add(new Vector3(-2.5f, 1, -16));
        Positions.Add(new Vector3(2.5f, 1, -16));
        Positions.Add(new Vector3(-2.5f, 3, -16));
        Positions.Add(new Vector3(2.5f, 3, -16));
        Positions.Add(new Vector3(2.5f, 5, -16));
        Positions.Add(new Vector3(-2.5f, 5, -16));
        Positions.Add(new Vector3(2.5f, 7, -16));
        Positions.Add(new Vector3(-2.5f, 7, -16));
        Positions.Add(new Vector3(-7.5f, 1, -16));
        Positions.Add(new Vector3(7.5f, 1, -16));
        Positions.Add(new Vector3(-7.5f, 3, -16));
        Positions.Add(new Vector3(7.5f, 3, -16));
        Positions.Add(new Vector3(7.5f, 5, -16));
        Positions.Add(new Vector3(-7.5f, 5, -16));
        Positions.Add(new Vector3(7.5f, 7, -16));
        Positions.Add(new Vector3(-7.5f, 7, -16));
        Positions.Add(new Vector3(-12.5f, 1, -16));
        Positions.Add(new Vector3(12.5f, 1, -16));
        Positions.Add(new Vector3(-12.5f, 3, -16));
        Positions.Add(new Vector3(12.5f, 3, -16));
        Positions.Add(new Vector3(12.5f, 5, -16));
        Positions.Add(new Vector3(-12.5f, 5, -16));
        Positions.Add(new Vector3(12.5f, 7, -16));
        Positions.Add(new Vector3(-12.5f, 7, -16));

        //todas están disponibles al principio
        foreach (Vector3 position in Positions)
        {
            AvailablePositions.Add(true);
        }
    }
#endregion

    //obtengo una posición random en el plano
    public int ObtainRandomPositionIndex()
    {
        //si no hay mas disponibles tiro exception
        if (NoPositionsLeft())
        {
            Debug.Log("Arrojo excepcion: No hay más posiciones disponibles");
            throw (new NoPositionsLeftException("No hay más posiciones disponibles"));
        }
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
    public bool NoPositionsLeft()
    {
        foreach (bool available in AvailablePositions)
        {
            if (available)
                return false;
        }
        return true;
    }
}
