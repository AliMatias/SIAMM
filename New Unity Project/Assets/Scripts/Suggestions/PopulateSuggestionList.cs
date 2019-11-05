using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

public class PopulateSuggestionList : MonoBehaviour
{
    // seteados desde unity
    public GameObject listItem;
    public GameObject content;

    private SuggestionManager suggestionManager;
    private MoleculeManager moleculeManager;
    private MaterialManager materialManager;
    private QryMoleculas qryMolecule;
    private QryMaterials qryMaterial;

    private UIPopup popup = null;

    private List<MoleculeData> allMolecules = new List<MoleculeData>();
    private List<MaterialData> allMaterials = new List<MaterialData>();

    private List<MoleculeData> fullyMatchedMolecules = new List<MoleculeData>();
    private List<MoleculeData> partiallyMatchedMolecules = new List<MoleculeData>();
    private List<MaterialData> fullyMatchedMaterials = new List<MaterialData>();
    private List<MaterialData> partiallyMatchedMaterials = new List<MaterialData>();

    private MoleculeData selectedMolecule = null;
    private MaterialData selectedMaterial = null;

    public MoleculeData SelectedMolecule { get => selectedMolecule; set => selectedMolecule = value; }
    public MaterialData SelectedMaterial { get => selectedMaterial; set => selectedMaterial = value; }

    // Use this for initialization
    void Start()
    {
        popup = FindObjectOfType<UIPopup>();
        suggestionManager = FindObjectOfType<SuggestionManager>();
        materialManager = FindObjectOfType<MaterialManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();

        GameObject go = new GameObject();
        go.AddComponent<QryMoleculas>();
        go.AddComponent<QryMaterials>();
        qryMolecule = go.GetComponent<QryMoleculas>();
        qryMaterial = go.GetComponent<QryMaterials>();
        try
        {
            allMolecules = qryMolecule.GetAllMolecules();
            allMaterials = qryMaterial.GetAllMaterials();
        }
        catch (Exception e)
        {
            Debug.LogError("PopulateSuggestionList :: Error getting data from database: " + e.StackTrace);
            popup.MostrarPopUp("Error", "Error cargando sugerencias");
        }
    }

    public void UpdateList()
    {
        ClearList();
        FilterMolecules();
        FilterMaterials();
        LoadList();
    }

    private void FilterMaterials()
    {
        if (suggestionManager.MaterialsFullyMatched != null && suggestionManager.MaterialsFullyMatched.Count > 0)
        {
            fullyMatchedMaterials = allMaterials.FindAll(material => suggestionManager.MaterialsFullyMatched.Contains(material.Id));
        }

        if (suggestionManager.MaterialsPartiallyMatched != null && suggestionManager.MaterialsPartiallyMatched.Count > 0)
        {
            partiallyMatchedMaterials = allMaterials.FindAll(material => suggestionManager.MaterialsPartiallyMatched.Contains(material.Id));
        }
    }

    private void FilterMolecules()
    {
        if (suggestionManager.MoleculesFullyMatched != null && suggestionManager.MoleculesFullyMatched.Count > 0)
        {
            fullyMatchedMolecules = allMolecules.FindAll(molecule => suggestionManager.MoleculesFullyMatched.Contains(molecule.Id));
            fullyMatchedMolecules.Sort(SortMoleculesByDiferenciaElectronegatividadDesc);
        }

        if (suggestionManager.MoleculesPartiallyMatched != null && suggestionManager.MoleculesPartiallyMatched.Count > 0)
        {
            partiallyMatchedMolecules = allMolecules.FindAll(molecule => suggestionManager.MoleculesPartiallyMatched.Contains(molecule.Id));
            partiallyMatchedMolecules.Sort(SortMoleculesByDiferenciaElectronegatividadDesc);
        }
    }

    private void LoadList()
    {
        if (fullyMatchedMolecules.Count > 0)
        {
            fullyMatchedMolecules.ForEach(moleculeData => LoadItemToList(moleculeData, true));
        }
        if (fullyMatchedMaterials.Count > 0)
        {
            fullyMatchedMaterials.ForEach(materialData => LoadItemToList(materialData, true));
        }

        if (partiallyMatchedMolecules.Count > 0)
        {
            partiallyMatchedMolecules.ForEach(moleculeData => LoadItemToList(moleculeData, false));
        }
        if (partiallyMatchedMaterials.Count > 0)
        {
            partiallyMatchedMaterials.ForEach(materialData => LoadItemToList(materialData, false));
        }
    }

    /**
     * Carga una molecula a la lista
     */
    public void LoadItemToList(MoleculeData molecule, bool isBold)
    {
        // crea un nuevo item en la lista
        var itemList = Instantiate(listItem);
        itemList.transform.SetParent(content.transform);
        itemList.transform.localPosition = Vector3.zero;
        //mostrara la formula + tradicional nom
        itemList.GetComponentInChildren<TextMeshProUGUI>().text = molecule.ToStringToList;
        if (isBold) itemList.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;

        // le agrega comportamiento al componente button del texto seleccionado
        itemList.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                SelectItem(molecule, itemList);
            }
        );
    }

    /**
     * Carga un material a la lista
     */
    public void LoadItemToList(MaterialData material, bool isBold)
    {
        // crea un nuevo item en la lista
        var itemList = Instantiate(listItem);
        itemList.transform.SetParent(content.transform);
        itemList.transform.localPosition = Vector3.zero;
        itemList.GetComponentInChildren<TextMeshProUGUI>().text = material.ToStringToList;
        if (isBold) itemList.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;

        // le agrega comportamiento al componente button del texto seleccionado
        itemList.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                SelectItem(material, itemList);
            }
        );
    }

    /**
     * Selecciona una molecula de la lista, marcandolo en azul
     */
    public void SelectItem(MoleculeData molecule, GameObject selectedItem)
    {
        DeselectAll();
        // selecciono el que quiero o lo deselecciono si ya estaba seleccionado
        if (selectedMolecule == molecule)
        {
            selectedMolecule = null;
        }
        else
        {
            selectedMaterial = null;
            selectedMolecule = molecule;
            selectedItem.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
        }
    }

    /**
     * Selecciona un material de la lista, marcandolo en azul
     */
    public void SelectItem(MaterialData material, GameObject selectedItem)
    {
        DeselectAll();
        // selecciono el que quiero o lo deselecciono si ya estaba seleccionado
        if (selectedMaterial == material)
        {
            selectedMaterial = null;
        }
        else
        {
            selectedMolecule = null;
            selectedMaterial = material;
            selectedItem.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
        }
    }

    /**
     * Agrega el item seleccionado al workspace
     */
    public void AddItem()
    {
        if (selectedMolecule != null)
        {
            suggestionManager.spawnSuggestedMolecule(selectedMolecule);
        }
        else if (selectedMaterial != null)
        {
            suggestionManager.spawnSuggestedMaterial(selectedMaterial);
        }
    }

    /**
     * Deselecciona todos los elementos de la lista
     */
    private void DeselectAll()
    {
        int childCount = content.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            child.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0x32, 0x32, 0x32, 0xFF);
        }
    }

    /**
    * Elimina todos los elementos de la lista
    */
    private void ClearList()
    {
        selectedMolecule = null;
        selectedMaterial = null;
        fullyMatchedMolecules = new List<MoleculeData>();
        partiallyMatchedMolecules = new List<MoleculeData>();
        fullyMatchedMaterials = new List<MaterialData>();
        partiallyMatchedMaterials = new List<MaterialData>();

        int childCount = content.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    /**
     * Ordena las moleculas por diferencia de electronegatividad de mayor a menor
     */
    private int SortMoleculesByDiferenciaElectronegatividadDesc(MoleculeData moleculeA, MoleculeData moleculeB)
    {
        if (moleculeA.DiferenciaElectronegatividad == null && moleculeB.DiferenciaElectronegatividad == null) return 0;
        if (moleculeA.DiferenciaElectronegatividad == null) return -1;
        if (moleculeB.DiferenciaElectronegatividad == null) return 1;
        return moleculeA.DiferenciaElectronegatividad > moleculeB.DiferenciaElectronegatividad ? -1 : 1;
    }
}
