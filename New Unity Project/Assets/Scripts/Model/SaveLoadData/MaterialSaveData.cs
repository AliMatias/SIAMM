using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialSaveData
{
    public int materialId;
    public int position;
    public string name;
    public string modelFile;

    public MaterialSaveData(int materialId, int position, string name, string modelFile)
    {
        this.materialId = materialId;
        this.position = position;
        this.name = name;
        this.modelFile = modelFile;
    }
}