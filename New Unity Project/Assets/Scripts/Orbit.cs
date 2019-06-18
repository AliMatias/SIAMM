using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class Orbit
{
    public int Number { get; set; }
    public string Name { get; set; }
    public int MaxElectrons { get; set; }
    public Vector3 Position { get; set; }
    public List<GameObject> ElectronList { get; }

    public Orbit(int number, string name, int maxElectrons, Vector3 position)
    {
        Number = number;
        Name = name;
        MaxElectrons = maxElectrons;
        Position = position;
        ElectronList = new List<GameObject>();
    }

    public List<GameObject> AddElectron(GameObject electron)
    {
        if (!isCompleted())
        {
            ElectronList.Add(electron);
        }
        return ElectronList;
    }

    public GameObject RemoveLastElectron()
    {
        if (ElectronList.Count > 0)
        {
            GameObject toDelete = ElectronList.LastOrDefault();
            ElectronList.Remove(toDelete);
            return toDelete;
        }
        return null;
    }

    public bool isCompleted()
    {
        return ElectronList.Count >= MaxElectrons;
    }
}