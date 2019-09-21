using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<AtomSaveData> atoms;
    public List<MoleculeSaveData> molecules;

    public SaveData(List<AtomSaveData> atoms, List<MoleculeSaveData> molecules)
    {
        this.atoms = atoms;
        this.molecules = molecules;
    }
}
