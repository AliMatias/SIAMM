using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/**
 * Subcapa de electrones (s, p, d, f, g, h, i)
 * https://es.wikipedia.org/wiki/Capa_electr%C3%B3nica#Subcapas
 */
public class ElectronSubshell
{
    private string name;
    private int maxElectrons;
    private List<GameObject> electronList;

    public string Name { get => name; set => name = value; }
    public int MaxElectrons { get => maxElectrons; set => maxElectrons = value; }
    public List<GameObject> ElectronList { get => electronList; }

    public ElectronSubshell(string name, int maxElectrons)
    {
        this.name = name;
        this.maxElectrons = maxElectrons;
        this.electronList = new List<GameObject>();
    }

    public List<GameObject> AddElectron(GameObject electron)
    {
        if (!isCompleted())
        {
            electronList.Add(electron);
        }
        return electronList;
    }

    public GameObject RemoveElectron(int index)
    {
        if (electronList.Count > 0 && index < electronList.Count)
        {
            GameObject toDelete = electronList[index];
            electronList.Remove(toDelete);
            return toDelete;
        }
        return null;
    }

    public GameObject RemoveLastElectron()
    {
        if (electronList.Count > 0)
        {
            GameObject toDelete = electronList.LastOrDefault();
            electronList.Remove(toDelete);
            return toDelete;
        }
        return null;
    }

    public bool isCompleted()
    {
        return electronList.Count >= maxElectrons;
    }
}