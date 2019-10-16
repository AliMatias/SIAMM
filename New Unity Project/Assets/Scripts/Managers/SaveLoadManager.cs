using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SFB;

public class SaveLoadManager : MonoBehaviour
{
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private MaterialManager materialManager;
    private PopulateMoleculeList populateMoleculeList;
    private string savePath;

    private void Awake() {
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        materialManager = FindObjectOfType<MaterialManager>();
        populateMoleculeList = FindObjectOfType<PopulateMoleculeList>();
        savePath = Application.dataPath + "/save.json";
    }

    public void SaveAs(){
        string savingPath = StandaloneFileBrowser.SaveFilePanel("Save File", "", "Save", "json");
        if(savingPath != ""){
            Debug.Log("Guardando en: " + savingPath);
            savePath = savingPath;
            Save();
        }
    }

    public void OpenFile(){
        string[] openingPath = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false);
        if(openingPath.Length > 0 && openingPath[0] != ""){
            Debug.Log("Abriendo archivo: " + openingPath[0]);
            savePath = openingPath[0];
            Load();
        }
    }

    public void Save(){
        List<AtomSaveData> atomSaveData = ObtainAtomsData();
        List<MoleculeSaveData> moleculeSaveData = ObtainMoleculesData();
        List<MaterialSaveData> materialSaveData = ObtainMaterialsData();
        SaveData save = new SaveData(atomSaveData, moleculeSaveData, materialSaveData);
        string json = JsonUtility.ToJson(save);
        Debug.Log("Game Saved! " + json);
        File.WriteAllText(savePath, json);
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
            moleculeSaveData.Add(new MoleculeSaveData(molecule.MoleculeId, molecule.MoleculeIndex, 
                molecule.MoleculeName));
        }
        return moleculeSaveData;
    }

    private List<MaterialSaveData> ObtainMaterialsData(){
        List<MaterialObject> materials = materialManager.Materials;
        List<MaterialSaveData> materialSaveData = new List<MaterialSaveData>();
        foreach(MaterialObject material in materials){
            materialSaveData.Add(new MaterialSaveData(material.MaterialId, material.MaterialIndex, 
                material.MaterialName, material.ModelFile));
        }
        return materialSaveData;
    }

    public void Load(){
        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(savePath));
        atomManager.DeleteAllAtoms();
        moleculeManager.DeleteAllMolecules();
        materialManager.DeleteAllMaterials();
        foreach(AtomSaveData atomData in save.atoms){
            atomManager.SpawnFromSaveData(atomData);
        }
        foreach(MoleculeSaveData moleculeData in save.molecules){
            populateMoleculeList.AddMolecule(moleculeData.name, moleculeData.position, 
                moleculeData.moleculeId);
        }
        foreach(MaterialSaveData materialData in save.materials){
            materialManager.SpawnMaterialFromSave(materialData.position, materialData.materialId, 
                materialData.name, materialData.modelFile);
        }
    }
}
