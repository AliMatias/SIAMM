using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<AtomSaveData> atoms;
    public List<MoleculeSaveData> molecules;
    public List<MaterialSaveData> materials;

    public SaveData(List<AtomSaveData> atoms, List<MoleculeSaveData> molecules, List<MaterialSaveData> materials)
    {
        this.atoms = atoms;
        this.molecules = molecules;
        this.materials = materials;
    }
}
