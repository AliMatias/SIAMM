using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulateMoleculeList : MonoBehaviour
{
    // seteados desde unity
    public GameObject moleculeItem;
    public GameObject content;

    private DBManager DBManager;
    private List<MoleculeData> moleculeList = new List<MoleculeData>();
    public MoleculeData SelectedMolecule { get; set; } = null;

    void Start()
    {
        DBManager = FindObjectOfType<DBManager>();
        moleculeList = DBManager.GetAllMolecules();
        // cargo todas las moleculas a la lista
        foreach (MoleculeData molecule in moleculeList)
        {
            LoadMoleculeToList(molecule);
        }
    }

    public void LoadMoleculeToList(MoleculeData molecule)
    {
        // crea un nuevo item en la lista
        var itemList = Instantiate(moleculeItem);
        itemList.transform.parent = content.transform;
        itemList.transform.localPosition = Vector3.zero;
        itemList.GetComponentInChildren<TextMeshProUGUI>().text = molecule.Formula;

        // le agrega comportamiento al componente button del texto seleccionado
        itemList.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                SelectMolecule(molecule, itemList);
                Debug.Log("Clicked: " + molecule.ToString);
            }
        );
    }

    public void SelectMolecule(MoleculeData molecule, GameObject selectedItem)
    {
        // deselecciono todos los elementos de la lista
        int childCount = content.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            child.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0x32, 0x32, 0x32, 0xFF);
        }

        // selecciono el que quiero o lo deselecciono si ya estaba seleccionado
        if (SelectedMolecule == molecule)
        {
            SelectedMolecule = null;
        }
        else
        {
            SelectedMolecule = molecule;
            selectedItem.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
        }
    }
}