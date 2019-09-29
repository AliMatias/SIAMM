using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoleculeSaveData
{
    public int moleculeId;
    public int position;
    public string name;

    public MoleculeSaveData(int moleculeId, int position, string name)
    {
        this.moleculeId = moleculeId;
        this.position = position;
        this.name = name;
    }
}
