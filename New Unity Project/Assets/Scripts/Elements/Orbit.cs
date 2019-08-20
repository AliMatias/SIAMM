using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class Orbit
{
    private int number;
    private string name;
    private int maxElectrons;
    private Vector3 position;
    private GameObject orbitCircle;
    private List<ElectronSubshell> electronSubshells;

    public int Number { get => number; set => number = value; }
    public string Name { get => name; set => name = value; }
    public int MaxElectrons { get => maxElectrons; set => maxElectrons = value; }
    public Vector3 Position { get => position; set => position = value; }
    public GameObject OrbitCircle { get => orbitCircle; set => orbitCircle = value; }
    public List<ElectronSubshell> ElectronSubshells { get => electronSubshells; }

    public Orbit(int number, string name, int maxElectrons, Vector3 position, GameObject orbitCircle)
    {
        this.number = number;
        this.name = name;
        this.maxElectrons = maxElectrons;
        this.position = position;
        this.orbitCircle = orbitCircle;
        this.electronSubshells = new List<ElectronSubshell>();
    }

    public ElectronSubshell GetElectronSubshell(string subshellName)
    {
        foreach (ElectronSubshell electronSubshell in electronSubshells)
        {
            if (electronSubshell.Name.Equals(subshellName))
            {
                return electronSubshell;
            }
        }

        return null;
    }
}