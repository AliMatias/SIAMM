using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private AtomManager atomManager;

    private void Awake() {
        atomManager = FindObjectOfType<AtomManager>();
    }

    public void Save(){
        List<Atom> atoms = atomManager.AtomsList;
        List<AtomSaveData> atomSaveData = new List<AtomSaveData>();
        foreach(Atom atom in atoms){
            atomSaveData.Add(new AtomSaveData(atom.ProtonCounter, atom.NeutronCounter, 
            atom.ElectronCounter, atom.AtomIndex));
        }
        SaveData save = new SaveData(atomSaveData);
        string json = JsonUtility.ToJson(save);
        Debug.Log("Game Saved! " + json);
        File.WriteAllText(Application.dataPath + "save.json", json);
    }

    public void Load(){
        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "save.json"));
        atomManager.DeleteAllAtoms();
        foreach(AtomSaveData atomData in save.atoms){
            atomManager.SpawnFromSaveData(atomData);
        }
    }
}
