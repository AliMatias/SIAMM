using System;
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
    private QryMoleculas qryMolecule;

    private InputField inputFilter;

    private List<MoleculeData> moleculeList = new List<MoleculeData>();
    public MoleculeData SelectedMolecule { get; set; } = null;

    private UIPopup popup = null;

    void Start()
    {
        // arranca oculto y desactivado
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.SetActive(false);

        popup = FindObjectOfType<UIPopup>();
        inputFilter = gameObject.GetComponentInChildren<InputField>();

        GameObject go = new GameObject();
        go.AddComponent<QryMoleculas>();
        qryMolecule = go.GetComponent<QryMoleculas>();

        moleculeManager = FindObjectOfType<MoleculeManager>();
        
        try
        {
            moleculeList = qryMolecule.GetAllMolecules();
        }
        catch (Exception e)
        {
            Debug.LogError("PopulateMoleculeList :: Ocurrio un error al buscar Todas las Moleculas de la Base: " + e.Message);
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Todas las Moleculas de la Base");
            return;
        }

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
        itemList.transform.SetParent(content.transform);
        itemList.transform.localPosition = Vector3.zero;
        //mostrara la formula + tradicional nom
        itemList.GetComponentInChildren<TextMeshProUGUI>().text = molecule.ToStringToList;

        // le agrega comportamiento al componente button del texto seleccionado
        itemList.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                SelectMolecule(molecule, itemList);
                //no va popup
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
            List<AtomInMolPositionData> atomsPosition = qryMolecule.GetElementPositions(SelectedMolecule.Id);
            moleculeManager.SpawnMolecule(atomsPosition, SelectedMolecule.ToStringToList);
        }
    }

    /*
     * Elimina el contenido de la lista y la vuelve a popular con las moleculas filtradas
     * Solo filtra por nomenclatura tradicional (ej. "Agua") o formula molecular (ej. "H2O")
     * Es case insensitive e ignora tildes.
     * Permite que al escribir "oxido" encuentre moléculas como "Óxido cuproso (Cu2O)".
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