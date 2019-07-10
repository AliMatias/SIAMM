﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulateMoleculeList : MonoBehaviour
{
    // seteados desde unity
    public GameObject moleculeItem;
    public GameObject content;

    private MoleculeManager moleculeManager;
    private DBManager DBManager;

    private InputField inputFilter;

    private List<MoleculeData> moleculeList = new List<MoleculeData>();
    public MoleculeData SelectedMolecule { get; set; } = null;

    void Start()
    {
        // arranca oculto y desactivado
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.SetActive(false);

        inputFilter = gameObject.GetComponentInChildren<InputField>();
        DBManager = FindObjectOfType<DBManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        moleculeList = DBManager.GetAllMolecules();
        // cargo todas las moleculas a la lista
        foreach (MoleculeData molecule in moleculeList)
        {
            LoadMoleculeToList(molecule);
        }
    }

    /**
     * Carga una molecula a la lista
     */
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

    /**
     * Selecciona una molecula de la lista, marcandola en azul
     */
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

    /**
     * Agrega la molecula seleccionada al workspace
     */
    public void AddMolecule()
    {
        if (SelectedMolecule != null)
        {
            List<AtomInMolPositionData> atomsPosition = DBManager.GetElementPositions(SelectedMolecule.Id);
            moleculeManager.SpawnMolecule(atomsPosition, SelectedMolecule.ToString);
        }
    }

    /*
     * Elimina el contenido de la lista y la vuelve a popular con las moleculas filtradas
     * Solo filtra por nomenclatura tradicional (ej. "Agua") o formula molecular (ej. "H2O")
     * Es case insensitive e ignora tildes.
     * Permite que escribir al "oxido" encuentre moléculas como "Óxido cuproso (Cu2O)".
     * Es llamado con el evento OnValueChanged del InputField de la lista de moleculas
     * Busca mientras el usuario escribe
     */
    public void FilterMolecules()
    {
        if (inputFilter != null)
        {
            string searchQuery = EliminarTildes(inputFilter.text.ToUpper());
            ClearList();
            foreach (MoleculeData molecule in moleculeList)
            {
                if (searchQuery == "" ||
                    EliminarTildes(molecule.Formula).ToUpper().Contains(searchQuery) ||
                    EliminarTildes(molecule.TraditionalNomenclature).ToUpper().Contains(searchQuery))
                {
                    LoadMoleculeToList(molecule);
                }
            }
        }
    }

    /**
     * Elimina todos los elementos de la lista
     */
    private void ClearList()
    {
        SelectedMolecule = null;
        int childCount = content.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    /**
     * Este metodo sirve para eliminar los tildes de un string
     * Ademas de tildes todos los caracteres Unicode de categoria NonSpacing Mark
     * Especificados aca: https://www.fileformat.info/info/unicode/category/Mn/list.htm
     */
    public string EliminarTildes(String s)
    {
        string normalizar = s.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < normalizar.Length; i++)
        {
            Char c = normalizar[i];
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }
}