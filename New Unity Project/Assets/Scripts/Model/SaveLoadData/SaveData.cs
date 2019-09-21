using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<AtomSaveData> atoms;

    public SaveData(List<AtomSaveData> atoms)
    {
        this.atoms = atoms;
    }
    //private MoleculeSaveData[] molecules;

}
