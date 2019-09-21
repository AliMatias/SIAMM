using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoleculeSaveData
{
    public int moleculeId;
    public int position;

    public MoleculeSaveData(int moleculeId, int position)
    {
        this.moleculeId = moleculeId;
        this.position = position;
    }
}
