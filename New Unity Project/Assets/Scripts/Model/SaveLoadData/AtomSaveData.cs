using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AtomSaveData
{
    public int neutrons;
    public int protons;
    public int electrons;
    public int position;

    public AtomSaveData(int protons, int neutrons, int electrons, int position)
    {
        this.neutrons = neutrons;
        this.protons = protons;
        this.electrons = electrons;
        this.position = position;
    }
}
