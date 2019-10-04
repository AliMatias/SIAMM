using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulateMaterialList : MonoBehaviour
{
    // seteados desde unity
    public GameObject materialItem;
    public GameObject content;

    private MaterialManager materialManager;
    private QryMaterials qryMaterials;

    private InputField inputFilter;

    private List<MaterialData> materialList = new List<MaterialData>();
    public MaterialData SelectedMaterial { get; set; } = null;

    private UIPopup popup = null;

    void Start()
    {
        popup = FindObjectOfType<UIPopup>();
        inputFilter = gameObject.GetComponentInChildren<InputField>();

        GameObject go = new GameObject();
        go.AddComponent<QryMaterials>();
        qryMaterials = go.GetComponent<QryMaterials>();

        materialManager = FindObjectOfType<MaterialManager>();
        
        try
        {
            materialList = qryMaterials.GetAllMaterials();
        }
        catch (Exception e)
        {
            Debug.LogError("PopulateMaterialList :: Ocurrió un error al buscar Todos los Materiales de la Base: " + e.Message);
            popup.MostrarPopUp("Materiales Qry DB", "Error Obteniendo Todos los Materiales de la Base");
            return;
        }

        // cargo todos los materiales a la lista
        foreach (MaterialData material in materialList)
        {
            LoadMaterialToList(material);
        }
    }

    /**
     * Carga un material a la lista
     */
    public void LoadMaterialToList(MaterialData material)
    {
        // crea un nuevo item en la lista
        var itemList = Instantiate(materialItem);
        itemList.transform.SetParent(content.transform);
        itemList.transform.localPosition = Vector3.zero;
        itemList.GetComponentInChildren<TextMeshProUGUI>().text = material.Name;

        // le agrega comportamiento al componente button del texto seleccionado
        itemList.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                SelectMaterial(material, itemList);
                Debug.Log("Clicked: " + material.Name);
            }
        );
    }

    /**
     * Selecciona un material de la lista, marcandolo en azul
     */
    public void SelectMaterial(MaterialData material, GameObject selectedItem)
    {
        // deselecciono todos los elementos de la lista
        int childCount = content.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject child = content.transform.GetChild(i).gameObject;
            child.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0x32, 0x32, 0x32, 0xFF);
        }

        // selecciono el que quiero o lo deselecciono si ya estaba seleccionado
        if (SelectedMaterial == material)
        {
            SelectedMaterial = null;
        }
        else
        {
            SelectedMaterial = material;
            selectedItem.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
        }
    }
 
    /**
     * Agrega el material seleccionado al workspace
     */
    public void AddMaterial()
    {
        if (SelectedMaterial != null)
        {
            materialManager.SpawnMaterial(SelectedMaterial);
        }
    }

    /*
     * Elimina el contenido de la lista y la vuelve a popular con los materiales filtrados
     * Es case insensitive e ignora tildes.
     * Es llamado con el evento OnValueChanged del InputField de la lista de moleculas
     * Busca mientras el usuario escribe
     */
    public void FilterMaterials()
    {
        if (inputFilter != null)
        {
            string searchQuery = EliminarTildes(inputFilter.text.ToUpper());
            ClearList();
            foreach (MaterialData material in materialList)
            {
                if (searchQuery == "" ||
                    EliminarTildes(material.Name).ToUpper().Contains(searchQuery))
                {
                    LoadMaterialToList(material);
                }
            }
        }
    }

    /**
     * Elimina todos los elementos de la lista
     */
    private void ClearList()
    {
        SelectedMaterial = null;
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