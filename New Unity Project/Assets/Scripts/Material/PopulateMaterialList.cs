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
 
}