using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SuggestionManager : MonoBehaviour
{
    private QryMoleculas qryMolecules;
    private QryMaterials qryMaterials;
    private QryElementos qryElementos;
    private UIPopup popup;
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private MaterialManager materialManager;
    private PopulateSuggestionList populateSuggestionList;

    private List<MoleculeElementsMapping> moleculeMappingList = new List<MoleculeElementsMapping>();
    private List<MaterialMappingData> materialMappingList = new List<MaterialMappingData>();

    private List<int> moleculesFullyMatched = new List<int>();
    private List<int> moleculesPartiallyMatched = new List<int>();
    private List<int> materialsFullyMatched = new List<int>();
    private List<int> materialsPartiallyMatched = new List<int>();

    public List<int> MoleculesFullyMatched { get => moleculesFullyMatched; set => moleculesFullyMatched = value; }
    public List<int> MoleculesPartiallyMatched { get => moleculesPartiallyMatched; set => moleculesPartiallyMatched = value; }
    public List<int> MaterialsFullyMatched { get => materialsFullyMatched; set => materialsFullyMatched = value; }
    public List<int> MaterialsPartiallyMatched { get => materialsPartiallyMatched; set => materialsPartiallyMatched = value; }

    void Start()
    {
        GameObject go = new GameObject();
        go.AddComponent<QryMoleculas>();
        go.AddComponent<QryMaterials>();
        go.AddComponent<QryElementos>();
        qryMolecules = go.GetComponent<QryMoleculas>();
        qryMaterials = go.GetComponent<QryMaterials>();
        qryElementos = go.GetComponent<QryElementos>();
        popup = FindObjectOfType<UIPopup>();

        populateSuggestionList = GameObject.Find("SuggestionList").GetComponent<PopulateSuggestionList>();

        moleculeManager = FindObjectOfType<MoleculeManager>();
        atomManager = FindObjectOfType<AtomManager>();
        materialManager = FindObjectOfType<MaterialManager>();

        try
        {
            moleculeMappingList = MoleculeElementsMapping.FormatMoleculeMapping(qryMolecules.GetAllMoleculeMappings());
            materialMappingList = qryMaterials.GetAllMaterialMappings();
            updateSuggestions();
        }
        catch (Exception e)
        {
            Debug.LogError("SuggestionManager :: Ocurrio un error al traer moleculas o materiales de la DB: " + e.StackTrace);
            popup.MostrarPopUp("ERROR", "Error inicializando motor de sugerencias");
        }
    }

    public void updateSuggestions()
    {
        moleculesFullyMatched = new List<int>();
        moleculesPartiallyMatched = new List<int>();
        materialsFullyMatched = new List<int>();
        materialsPartiallyMatched = new List<int>();

        // if selected filter mapping lists
        List<int> selectedAtoms = atomManager.GetSelectedAtomNumbers();
        List<int> selectedMolecules = moleculeManager.GetSelectedMoleculeIds();

        List<MoleculeElementsMapping> filteredMoleculeMappingList = new List<MoleculeElementsMapping>();
        List<MaterialMappingData> filteredMaterialMappingList = new List<MaterialMappingData>();

        if (selectedAtoms.Count > 0 && selectedMolecules.Count <= 0)
        {
            filteredMoleculeMappingList = FilterMoleculeMapping(selectedAtoms);
            filteredMaterialMappingList = FilterMaterialMapping(selectedAtoms, selectedMolecules);
            findMatchingMolecules(filteredMoleculeMappingList);
            findMatchingMaterials(filteredMaterialMappingList);
        }

        if (selectedMolecules.Count > 0 && selectedAtoms.Count <= 0)
        {
            filteredMaterialMappingList = FilterMaterialMapping(selectedAtoms, selectedMolecules);
            findMatchingMolecules(filteredMoleculeMappingList);
            findMatchingMaterials(filteredMaterialMappingList);
        }

        if (selectedAtoms.Count <= 0 && selectedMolecules.Count <= 0)
        {
            findMatchingMolecules(moleculeMappingList);
            findMatchingMaterials(materialMappingList);
        }

        populateSuggestionList.UpdateList();
    }

    private List<MoleculeElementsMapping> FilterMoleculeMapping(List<int> selectedAtoms)
    {
        return moleculeMappingList.FindAll(mapping =>
        {
            List<int> tempList = new List<int>(selectedAtoms);
            foreach (MoleculeElementAmountStruct element in mapping.Elements)
            {
                tempList.RemoveAll(atom => atom == element.IdElement);
            };
            return tempList.Count == 0;
        });
    }

    private List<MaterialMappingData> FilterMaterialMapping(List<int> selectedAtoms, List<int> selectedMolecules)
    {
        return materialMappingList.FindAll(mapping =>
        {
            if (mapping.IdElement > 0)
            {
                return selectedAtoms.Contains(mapping.IdElement);
            }

            if (mapping.IdMolecule > 0)
            {
                return selectedMolecules.Contains(mapping.IdMolecule);
            }
            return false;
        });
    }

    private void findMatchingMolecules(List<MoleculeElementsMapping> mappingList)
    {
        mappingList.ForEach(mapping =>
        {
            int elementsMatchedCount = 0;
            mapping.Elements.ForEach(element =>
            {
                int matchedAmount = atomManager.AtomsList.FindAll(atom => atom.ElementNumber == element.IdElement).Count;
                elementsMatchedCount = matchedAmount >= element.Amount ? elementsMatchedCount + element.Amount : elementsMatchedCount + matchedAmount;
            });
            if (elementsMatchedCount == mapping.totalElementsAmount())
            {
                moleculesFullyMatched.Add(mapping.IdMolecule);
            }
            else if (elementsMatchedCount > 0)
            {
                moleculesPartiallyMatched.Add(mapping.IdMolecule);
            }
        });
    }

    private void findMatchingMaterials(List<MaterialMappingData> mappingList)
    {
        mappingList.ForEach(mapping =>
        {
            int elementsMatchedCount = 0;
            if (mapping.IdElement > 0)
            {
                int matchedAmount = atomManager.AtomsList.FindAll(atom => atom.ElementNumber == mapping.IdElement).Count;
                elementsMatchedCount = matchedAmount >= mapping.Amount ? elementsMatchedCount + mapping.Amount : elementsMatchedCount + matchedAmount;
            }
            else if (mapping.IdMolecule > 0)
            {
                int matchedAmount = moleculeManager.Molecules.FindAll(molecule => molecule.MoleculeId == mapping.IdMolecule).Count;
                elementsMatchedCount = matchedAmount >= mapping.Amount ? elementsMatchedCount + mapping.Amount : elementsMatchedCount + matchedAmount;
            }

            if (elementsMatchedCount == mapping.Amount)
            {
                materialsFullyMatched.Add(mapping.IdMaterial);
            }
            else if (elementsMatchedCount > 0)
            {
                materialsPartiallyMatched.Add(mapping.IdMaterial);
            }
        });
    }

    public void spawnSuggestedMolecule(MoleculeData molecule)
    {
        try
        {
            if (!isMoleculeFullyMatched(molecule.Id))
            {
                showMissingElementsOfMolecule(molecule.Id);
                return;
            }
            deleteElementsOfMolecule(molecule.Id);
            List<AtomInMolPositionData> atomsPosition = qryMolecules.GetElementPositions(molecule.Id);
            moleculeManager.SpawnMolecule(atomsPosition, molecule.ToStringToList);
        }
        catch (Exception e)
        {
            Debug.LogError("SuggestionManager :: Error spawneando molecula: " + e.StackTrace);
            popup.MostrarPopUp("ERROR", "Error aplicando sugerencia");
        }
    }

    public void spawnSuggestedMaterial(MaterialData material)
    {
        try
        { 
            if (!isMaterialFullyMatched(material.Id))
            {
                showMissingElementsOfMaterial(material.Id);
                return;
            }
            deleteElementsOfMaterial(material.Id);
            materialManager.SpawnMaterial(material);
        }
        catch (Exception e)
        {
            Debug.LogError("SuggestionManager :: Error spawneando molecula: " + e.StackTrace);
            popup.MostrarPopUp("ERROR", "Error aplicando sugerencia");
        }
    }

    public bool isMaterialFullyMatched(int materialId)
    {
        return isFullyMatched(materialId, materialsFullyMatched);
    }

    public bool isMoleculeFullyMatched(int moleculeId)
    {
        return isFullyMatched(moleculeId, moleculesFullyMatched);
    }

    private bool isFullyMatched(int id, List<int> list)
    {
        return list.Find(item => item == id) > 0;
    }

    private void deleteElementsOfMolecule(int moleculeId)
    {
        MoleculeElementsMapping moleculeMapping = moleculeMappingList.Find(mapping => mapping.IdMolecule == moleculeId);
        if (moleculeMapping == null) return;

        moleculeMapping.Elements.ForEach(element =>
        {
            for (int i = 0; i < element.Amount; i++)
            {
                Atom foundAtom = atomManager.AtomsList.Find(atom => atom.ElementNumber == element.IdElement);
                if (foundAtom != null)
                {
                    atomManager.DeleteAtom(foundAtom.AtomIndex);
                }
            }
        });
    }

    private void deleteElementsOfMaterial(int materialId)
    {
        MaterialMappingData materialMapping = materialMappingList.Find(mapping => mapping.IdMaterial == materialId);
        if (materialMapping == null) return;
        if (materialMapping.IdElement > 0)
        {
            for (int i = 0; i < materialMapping.Amount; i++)
            {
                Atom foundAtom = atomManager.AtomsList.Find(atom => atom.ElementNumber == materialMapping.IdElement);
                if (foundAtom != null)
                {
                    atomManager.DeleteAtom(foundAtom.AtomIndex);
                }
            }
        }
        else if(materialMapping.IdMolecule > 0)
        {
            for (int i = 0; i < materialMapping.Amount; i++)
            {
                Molecule foundMolecule = moleculeManager.Molecules.Find(molecule => molecule.MoleculeId == materialMapping.IdMolecule);
                if (foundMolecule != null)
                {
                    moleculeManager.DeleteMolecule(foundMolecule);
                }
            }
        }        
    }

    private void showMissingElementsOfMolecule(int moleculeId)
    {
        MoleculeElementsMapping moleculeMapping = moleculeMappingList.Find(mapping => mapping.IdMolecule == moleculeId);
        if (moleculeMapping == null) return;

        int extraLines = 0;
        string message = "Para poder aplicar esta sugerencia falta cargar:";
        moleculeMapping.Elements.ForEach(element =>
        {
            List<Atom> foundAtoms = atomManager.AtomsList.FindAll(atom => atom.ElementNumber == element.IdElement);
            int missingAmount = foundAtoms.Count > 0 ? element.Amount - foundAtoms.Count : element.Amount;
            if (missingAmount > 0)
            {
                ElementTabPer elementTabPer = qryElementos.GetElementFromNro(element.IdElement);
                message += "\n" + missingAmount + " " + (missingAmount > 1 ? "átomos" : "átomo") + " de " + elementTabPer.Name;
                extraLines++;
            }
        });
        message += "\n(Tip: Podés usar la tabla periódica)";
        extraLines++;
        popup.MostrarPopUp("Atención!", message, extraLines + 1);
    }

    private void showMissingElementsOfMaterial(int materialId)
    {
        MaterialMappingData materialMapping = materialMappingList.Find(mapping => mapping.IdMaterial == materialId);
        if (materialMapping == null) return;

        int extraLines = 0;
        string message = "Para poder aplicar esta sugerencia falta cargar:";

        if (materialMapping.IdElement > 0)
        {
           List<Atom> foundAtoms = atomManager.AtomsList.FindAll(atom => atom.ElementNumber == materialMapping.IdElement);
            int missingAmount = foundAtoms.Count > 0 ? materialMapping.Amount - foundAtoms.Count : materialMapping.Amount;
            if (missingAmount > 0)
            {
                ElementTabPer elementTabPer = qryElementos.GetElementFromNro(materialMapping.IdElement);
                message += "\n" + missingAmount + " " + (missingAmount > 1 ? "átomos" : "átomo") + " de " + elementTabPer.Name;
                extraLines++;
            }
            message += "\n(Tip: Podés usar la tabla periódica)";
            extraLines++;
        }
        else if (materialMapping.IdMolecule > 0)
        {
            List<Molecule> foundMolecules = moleculeManager.Molecules.FindAll(molecule => molecule.MoleculeId == materialMapping.IdMolecule);
            int missingAmount = foundMolecules.Count > 0 ? materialMapping.Amount - foundMolecules.Count : materialMapping.Amount;
            if (missingAmount > 0)
            {
                MoleculeData moleculeData = qryMolecules.GetMoleculeById(materialMapping.IdMolecule);
                message += "\n" + missingAmount + " " + (missingAmount > 1 ? "moléculas" : "molécula") + " de " + moleculeData.ToStringToList;
                extraLines++;
            }
            message += "\n(Tip: Podés usar la lista de moléculas)";
            extraLines++;
        }
        popup.MostrarPopUp("Atención!", message, extraLines + 1);
    }
}
