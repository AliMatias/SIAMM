using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private PopulateMoleculeList populateMoleculeList;

    private void Awake() {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        populateMoleculeList = FindObjectOfType<PopulateMoleculeList>();
    }

    public void Save(){
        List<AtomSaveData> atomSaveData = ObtainAtomsData();
        List<MoleculeSaveData> moleculeSaveData = ObtainMoleculesData();
        SaveData save = new SaveData(atomSaveData, moleculeSaveData);
        string json = JsonUtility.ToJson(save);
        Debug.Log("Game Saved! " + json);
        File.WriteAllText(Application.dataPath + "save.json", json);
    }

    private List<AtomSaveData> ObtainAtomsData(){
        List<Atom> atoms = atomManager.AtomsList;
        List<AtomSaveData> atomSaveData = new List<AtomSaveData>();
        foreach(Atom atom in atoms){
            atomSaveData.Add(new AtomSaveData(atom.ProtonCounter, atom.NeutronCounter, 
            atom.ElectronCounter, atom.AtomIndex));
        }
        return atomSaveData;
    }

    private List<MoleculeSaveData> ObtainMoleculesData(){
        List<Molecule> molecules = moleculeManager.Molecules;
        List<MoleculeSaveData> moleculeSaveData = new List<MoleculeSaveData>();
        foreach(Molecule molecule in molecules){
            moleculeSaveData.Add(new MoleculeSaveData(molecule.MoleculeId, molecule.MoleculeIndex, molecule.MoleculeName));
        }
        return moleculeSaveData;
    }

    public void Load(){
        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "save.json"));
        atomManager.DeleteAllAtoms();
        moleculeManager.DeleteAllMolecules();
        foreach(AtomSaveData atomData in save.atoms){
            atomManager.SpawnFromSaveData(atomData);
        }
        foreach(MoleculeSaveData moleculeData in save.molecules){
            populateMoleculeList.AddMolecule(moleculeData.name, moleculeData.position, moleculeData.moleculeId);
        }
    }
}
