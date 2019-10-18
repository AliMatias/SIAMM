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
        Positions.Add(new Vector3(-2.5f, -0.5f, 0));
        Positions.Add(new Vector3(2.5f, -0.5f, 0));
        Positions.Add(new Vector3(-2.5f, 3, 0));
        Positions.Add(new Vector3(2.5f, 3, 0));
        Positions.Add(new Vector3(2.5f, 6.5f, 0));
        Positions.Add(new Vector3(-2.5f, 6.5f, 0));
        Positions.Add(new Vector3(2.5f, 10, 0));
        Positions.Add(new Vector3(-2.5f, 10, 0));
        Positions.Add(new Vector3(-7.5f, -0.5f, 0));
        Positions.Add(new Vector3(7.5f, -0.5f, 0));
        Positions.Add(new Vector3(-7.5f, 3, 0));
        Positions.Add(new Vector3(7.5f, 3, 0));
        Positions.Add(new Vector3(7.5f, 6.5f, 0));
        Positions.Add(new Vector3(-7.5f, 6.5f, 0));
        Positions.Add(new Vector3(7.5f, 10, 0));
        Positions.Add(new Vector3(-7.5f, 10, 0));
        Positions.Add(new Vector3(-12.5f, -0.5f, 0));
        Positions.Add(new Vector3(12.5f, -0.5f, 0));
        Positions.Add(new Vector3(-12.5f, 3, 0));
        Positions.Add(new Vector3(12.5f, 3, 0));
        Positions.Add(new Vector3(12.5f, 6.5f, 0));
        Positions.Add(new Vector3(-12.5f, 6.5f, 0));
        Positions.Add(new Vector3(12.5f, 10, 0));
        Positions.Add(new Vector3(-12.5f, 10, 0));

        //otros planos (solo cambian en Z)
        //Z=4
        Positions.Add(new Vector3(-2.5f, -0.5f, 4));
        Positions.Add(new Vector3(2.5f, -0.5f, 4));
        Positions.Add(new Vector3(-2.5f, 3, 4));
        Positions.Add(new Vector3(2.5f, 3, 4));
        Positions.Add(new Vector3(2.5f, 6.5f, 4));
        Positions.Add(new Vector3(-2.5f, 6.5f, 4));
        Positions.Add(new Vector3(2.5f, 10, 4));
        Positions.Add(new Vector3(-2.5f, 10, 4));
        Positions.Add(new Vector3(-7.5f, -0.5f, 4));
        Positions.Add(new Vector3(7.5f, -0.5f, 4));
        Positions.Add(new Vector3(-7.5f, 3, 4));
        Positions.Add(new Vector3(7.5f, 3, 4));
        Positions.Add(new Vector3(7.5f, 6.5f, 4));
        Positions.Add(new Vector3(-7.5f, 6.5f, 4));
        Positions.Add(new Vector3(7.5f, 10, 4));
        Positions.Add(new Vector3(-7.5f, 10, 4));
        Positions.Add(new Vector3(-12.5f, -0.5f, 4));
        Positions.Add(new Vector3(12.5f, -0.5f, 4));
        Positions.Add(new Vector3(-12.5f, 3, 4));
        Positions.Add(new Vector3(12.5f, 3, 4));
        Positions.Add(new Vector3(12.5f, 6.5f, 4));
        Positions.Add(new Vector3(-12.5f, 6.5f, 4));
        Positions.Add(new Vector3(12.5f, 10, 4));
        Positions.Add(new Vector3(-12.5f, 10, 4));

        //Z=-4
        Positions.Add(new Vector3(-2.5f, -0.5f, -4));
        Positions.Add(new Vector3(2.5f, -0.5f, -4));
        Positions.Add(new Vector3(-2.5f, 3, -4));
        Positions.Add(new Vector3(2.5f, 3, -4));
        Positions.Add(new Vector3(2.5f, 6.5f, -4));
        Positions.Add(new Vector3(-2.5f, 6.5f, -4));
        Positions.Add(new Vector3(2.5f, 10, -4));
        Positions.Add(new Vector3(-2.5f, 10, -4));
        Positions.Add(new Vector3(-7.5f, -0.5f, -4));
        Positions.Add(new Vector3(7.5f, -0.5f, -4));
        Positions.Add(new Vector3(-7.5f, 3, -4));
        Positions.Add(new Vector3(7.5f, 3, -4));
        Positions.Add(new Vector3(7.5f, 6.5f, -4));
        Positions.Add(new Vector3(-7.5f, 6.5f, -4));
        Positions.Add(new Vector3(7.5f, 10, -4));
        Positions.Add(new Vector3(-7.5f, 10, -4));
        Positions.Add(new Vector3(-12.5f, -0.5f, -4));
        Positions.Add(new Vector3(12.5f, -0.5f, -4));
        Positions.Add(new Vector3(-12.5f, 3, -4));
        Positions.Add(new Vector3(12.5f, 3, -4));
        Positions.Add(new Vector3(12.5f, 6.5f, -4));
        Positions.Add(new Vector3(-12.5f, 6.5f, -4));
        Positions.Add(new Vector3(12.5f, 10, -4));
        Positions.Add(new Vector3(-12.5f, 10, -4));

        //Z=8
        Positions.Add(new Vector3(-2.5f, -0.5f, 8));
        Positions.Add(new Vector3(2.5f, -0.5f, 8));
        Positions.Add(new Vector3(-2.5f, 3, 8));
        Positions.Add(new Vector3(2.5f, 3, 8));
        Positions.Add(new Vector3(2.5f, 6.5f, 8));
        Positions.Add(new Vector3(-2.5f, 6.5f, 8));
        Positions.Add(new Vector3(2.5f, 10, 8));
        Positions.Add(new Vector3(-2.5f, 10, 8));
        Positions.Add(new Vector3(-7.5f, -0.5f, 8));
        Positions.Add(new Vector3(7.5f, -0.5f, 8));
        Positions.Add(new Vector3(-7.5f, 3, 8));
        Positions.Add(new Vector3(7.5f, 3, 8));
        Positions.Add(new Vector3(7.5f, 6.5f, 8));
        Positions.Add(new Vector3(-7.5f, 6.5f, 8));
        Positions.Add(new Vector3(7.5f, 10, 8));
        Positions.Add(new Vector3(-7.5f, 10, 8));
        Positions.Add(new Vector3(-12.5f, -0.5f, 8));
        Positions.Add(new Vector3(12.5f, -0.5f, 8));
        Positions.Add(new Vector3(-12.5f, 3, 8));
        Positions.Add(new Vector3(12.5f, 3, 8));
        Positions.Add(new Vector3(12.5f, 6.5f, 8));
        Positions.Add(new Vector3(-12.5f, 6.5f, 8));
        Positions.Add(new Vector3(12.5f, 10, 8));
        Positions.Add(new Vector3(-12.5f, 10, 8));

        //Z=-8
        Positions.Add(new Vector3(-2.5f, -0.5f, -8));
        Positions.Add(new Vector3(2.5f, -0.5f, -8));
        Positions.Add(new Vector3(-2.5f, 3, -8));
        Positions.Add(new Vector3(2.5f, 3, -8));
        Positions.Add(new Vector3(2.5f, 6.5f, -8));
        Positions.Add(new Vector3(-2.5f, 6.5f, -8));
        Positions.Add(new Vector3(2.5f, 10, -8));
        Positions.Add(new Vector3(-2.5f, 10, -8));
        Positions.Add(new Vector3(-7.5f, -0.5f, -8));
        Positions.Add(new Vector3(7.5f, -0.5f, -8));
        Positions.Add(new Vector3(-7.5f, 3, -8));
        Positions.Add(new Vector3(7.5f, 3, -8));
        Positions.Add(new Vector3(7.5f, 6.5f, -8));
        Positions.Add(new Vector3(-7.5f, 6.5f, -8));
        Positions.Add(new Vector3(7.5f, 10, -8));
        Positions.Add(new Vector3(-7.5f, 10, -8));
        Positions.Add(new Vector3(-12.5f, -0.5f, -8));
        Positions.Add(new Vector3(12.5f, -0.5f, -8));
        Positions.Add(new Vector3(-12.5f, 3, -8));
        Positions.Add(new Vector3(12.5f, 3, -8));
        Positions.Add(new Vector3(12.5f, 6.5f, -8));
        Positions.Add(new Vector3(-12.5f, 6.5f, -8));
        Positions.Add(new Vector3(12.5f, 10, -8));
        Positions.Add(new Vector3(-12.5f, 10, -8));

        //Z=12
        Positions.Add(new Vector3(-2.5f, -0.5f, 12));
        Positions.Add(new Vector3(2.5f, -0.5f, 12));
        Positions.Add(new Vector3(-2.5f, 3, 12));
        Positions.Add(new Vector3(2.5f, 3, 12));
        Positions.Add(new Vector3(2.5f, 6.5f, 12));
        Positions.Add(new Vector3(-2.5f, 6.5f, 12));
        Positions.Add(new Vector3(2.5f, 10, 12));
        Positions.Add(new Vector3(-2.5f, 10, 12));
        Positions.Add(new Vector3(-7.5f, -0.5f, 12));
        Positions.Add(new Vector3(7.5f, -0.5f, 12));
        Positions.Add(new Vector3(-7.5f, 3, 12));
        Positions.Add(new Vector3(7.5f, 3, 12));
        Positions.Add(new Vector3(7.5f, 6.5f, 12));
        Positions.Add(new Vector3(-7.5f, 6.5f, 12));
        Positions.Add(new Vector3(7.5f, 10, 12));
        Positions.Add(new Vector3(-7.5f, 10, 12));
        Positions.Add(new Vector3(-12.5f, -0.5f, 12));
        Positions.Add(new Vector3(12.5f, -0.5f, 12));
        Positions.Add(new Vector3(-12.5f, 3, 12));
        Positions.Add(new Vector3(12.5f, 3, 12));
        Positions.Add(new Vector3(12.5f, 6.5f, 12));
        Positions.Add(new Vector3(-12.5f, 6.5f, 12));
        Positions.Add(new Vector3(12.5f, 10, 12));
        Positions.Add(new Vector3(-12.5f, 10, 12));

        //Z=-12
        Positions.Add(new Vector3(-2.5f, -0.5f, -12));
        Positions.Add(new Vector3(2.5f, -0.5f, -12));
        Positions.Add(new Vector3(-2.5f, 3, -12));
        Positions.Add(new Vector3(2.5f, 3, -12));
        Positions.Add(new Vector3(2.5f, 6.5f, -12));
        Positions.Add(new Vector3(-2.5f, 6.5f, -12));
        Positions.Add(new Vector3(2.5f, 10, -12));
        Positions.Add(new Vector3(-2.5f, 10, -12));
        Positions.Add(new Vector3(-7.5f, -0.5f, -12));
        Positions.Add(new Vector3(7.5f, -0.5f, -12));
        Positions.Add(new Vector3(-7.5f, 3, -12));
        Positions.Add(new Vector3(7.5f, 3, -12));
        Positions.Add(new Vector3(7.5f, 6.5f, -12));
        Positions.Add(new Vector3(-7.5f, 6.5f, -12));
        Positions.Add(new Vector3(7.5f, 10, -12));
        Positions.Add(new Vector3(-7.5f, 10, -12));
        Positions.Add(new Vector3(-12.5f, -0.5f, -12));
        Positions.Add(new Vector3(12.5f, -0.5f, -12));
        Positions.Add(new Vector3(-12.5f, 3, -12));
        Positions.Add(new Vector3(12.5f, 3, -12));
        Positions.Add(new Vector3(12.5f, 6.5f, -12));
        Positions.Add(new Vector3(-12.5f, 6.5f, -12));
        Positions.Add(new Vector3(12.5f, 10, -12));
        Positions.Add(new Vector3(-12.5f, 10, -12));

        //Z=16
        Positions.Add(new Vector3(-2.5f, -0.5f, 16));
        Positions.Add(new Vector3(2.5f, -0.5f, 16));
        Positions.Add(new Vector3(-2.5f, 3, 16));
        Positions.Add(new Vector3(2.5f, 3, 16));
        Positions.Add(new Vector3(2.5f, 6.5f, 16));
        Positions.Add(new Vector3(-2.5f, 6.5f, 16));
        Positions.Add(new Vector3(2.5f, 10, 16));
        Positions.Add(new Vector3(-2.5f, 10, 16));
        Positions.Add(new Vector3(-7.5f, -0.5f, 16));
        Positions.Add(new Vector3(7.5f, -0.5f, 16));
        Positions.Add(new Vector3(-7.5f, 3, 16));
        Positions.Add(new Vector3(7.5f, 3, 16));
        Positions.Add(new Vector3(7.5f, 6.5f, 16));
        Positions.Add(new Vector3(-7.5f, 6.5f, 16));
        Positions.Add(new Vector3(7.5f, 10, 16));
        Positions.Add(new Vector3(-7.5f, 10, 16));
        Positions.Add(new Vector3(-12.5f, -0.5f, 16));
        Positions.Add(new Vector3(12.5f, -0.5f, 16));
        Positions.Add(new Vector3(-12.5f, 3, 16));
        Positions.Add(new Vector3(12.5f, 3, 16));
        Positions.Add(new Vector3(12.5f, 6.5f, 16));
        Positions.Add(new Vector3(-12.5f, 6.5f, 16));
        Positions.Add(new Vector3(12.5f, 10, 16));
        Positions.Add(new Vector3(-12.5f, 10, 16));
        
        //Z=-16
        Positions.Add(new Vector3(-2.5f, -0.5f, -16));
        Positions.Add(new Vector3(2.5f, -0.5f, -16));
        Positions.Add(new Vector3(-2.5f, 3, -16));
        Positions.Add(new Vector3(2.5f, 3, -16));
        Positions.Add(new Vector3(2.5f, 6.5f, -16));
        Positions.Add(new Vector3(-2.5f, 6.5f, -16));
        Positions.Add(new Vector3(2.5f, 10, -16));
        Positions.Add(new Vector3(-2.5f, 10, -16));
        Positions.Add(new Vector3(-7.5f, -0.5f, -16));
        Positions.Add(new Vector3(7.5f, -0.5f, -16));
        Positions.Add(new Vector3(-7.5f, 3, -16));
        Positions.Add(new Vector3(7.5f, 3, -16));
        Positions.Add(new Vector3(7.5f, 6.5f, -16));
        Positions.Add(new Vector3(-7.5f, 6.5f, -16));
        Positions.Add(new Vector3(7.5f, 10, -16));
        Positions.Add(new Vector3(-7.5f, 10, -16));
        Positions.Add(new Vector3(-12.5f, -0.5f, -16));
        Positions.Add(new Vector3(12.5f, -0.5f, -16));
        Positions.Add(new Vector3(-12.5f, 3, -16));
        Positions.Add(new Vector3(12.5f, 3, -16));
        Positions.Add(new Vector3(12.5f, 6.5f, -16));
        Positions.Add(new Vector3(-12.5f, 6.5f, -16));
        Positions.Add(new Vector3(12.5f, 10, -16));
        Positions.Add(new Vector3(-12.5f, 10, -16));

        //todas están disponibles al principio
        foreach (Vector3 position in Positions)
        {
            AvailablePositions.Add(true);
        }
    }
#endregion

    //obtengo la proxima posicion disponible en el plano
    public int GetFirstAvailablePositionIndex()
    {
        int index = 0;
        foreach (bool available in AvailablePositions)
        {
            if (available) {
                AvailablePositions[index] = false;
                return index; 
            }
            index++;
        }

        Debug.Log("No hay más posiciones disponibles");
        throw (new NoPositionsLeftException("No hay más posiciones disponibles"));
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

    public bool OccupyPosition(int position){
        if(AvailablePositions[position]){
            AvailablePositions[position] = false;
            return true;
        }else{
            Debug.LogError("Posición " + position + " ya está ocupada.");
            return false;
        }
    }

    public void ResetAllPositions()
    {
        Positions = new List<Vector3>();
        AvailablePositions = new List<bool>();
        LoadPositions();
    }
}
