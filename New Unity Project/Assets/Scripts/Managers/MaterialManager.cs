using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MaterialManager : MonoBehaviour
{
    private PositionManager positionManager = PositionManager.Instance;
    private SelectionManager selectionManager;
    private CombinationManager combinationManager;
    private UIPopup popup;

    public MaterialObject materialPrefab;

    private List<MaterialObject> materials;

    //panel de info
    private MainInfoPanel mainInfoPanel;

    public List<MaterialObject> Materials { get => materials; } 

    public void Awake()
    {
        popup = FindObjectOfType<UIPopup>();
        selectionManager = FindObjectOfType<SelectionManager>();
        combinationManager = FindObjectOfType<CombinationManager>();
        materials = new List<MaterialObject>();
        mainInfoPanel = FindObjectOfType<MainInfoPanel>();
    }

    public void SpawnMaterial(MaterialData materialData)
    {
        MaterialObject newMaterial = GetMaterialPos();
        newMaterial.SetMaterialName(materialData.Name);
        newMaterial.MaterialId = materialData.Id;
        newMaterial.ModelFile = materialData.ModelFile;
        newMaterial.SpawnModel(Resources.Load("MaterialModels/" + materialData.ModelFile, typeof(GameObject)));

        //seteo info en panel inferior de elementos
        mainInfoPanel.SetInfoMaterial(materialData);
    }

    public void SpawnMaterialFromSave(int position, int id, string name, string modelFile){
        MaterialObject newMaterial = Instantiate<MaterialObject>(materialPrefab);
        newMaterial.transform.localPosition = positionManager.Positions[position];
        newMaterial.MaterialIndex = position;
        positionManager.OccupyPosition(position);
        materials.Add(newMaterial);
        newMaterial.SetMaterialName(name);
        newMaterial.MaterialId = id;
        newMaterial.ModelFile = modelFile;
        newMaterial.SpawnModel(Resources.Load("MaterialModels/" + modelFile, typeof(GameObject)));
    }

    private MaterialObject GetMaterialPos()
    {
        int position;
        try
        {
            position = positionManager.GetFirstAvailablePositionIndex();
        }
        catch (NoPositionsLeftException nple)
        {
            Debug.Log(nple.Message);
            throw;
        }

        MaterialObject newMaterial = Instantiate<MaterialObject>(materialPrefab);
        newMaterial.transform.localPosition = positionManager.Positions[position];
        newMaterial.MaterialIndex = position;

        materials.Add(newMaterial);
        return newMaterial;
    }

    public MaterialObject FindMaterialInList(int index)
    {
        foreach (MaterialObject material in materials)
        {
            if (material.MaterialIndex == index)
            {
                return material;
            }
        }
        return null;
    }

    // seleccionar material
    public void SelectMaterial(int index)
    {
        if(!combinationManager.CombineMode)
        {
            MaterialObject selectedMaterial = FindMaterialInList(index);
            selectionManager.SelectObject(selectedMaterial);
        } 
        else
        {
            Debug.Log("[MaterialManager] :: No se pueden seleccionar materiales en modo combinacion");
            popup.MostrarPopUp("Información", "No se pueden seleccionar materiales en modo combinacion");
        }
    }

    // BORRAR material seleccionado.
    public void DeleteSelectedMaterial()
    {
        List<int> selectedObjects = selectionManager.SelectedObjects;
        MaterialObject material = selectedObjects.Count > 0 ?
            FindMaterialInList(selectedObjects.Last()) :
            null;

        if (material == null)
        {
            Debug.Log("[MaterialManager] :: No hay ningún material seleccionado");
            popup.MostrarPopUp("Error", "No hay ningún material seleccionado");

            return;
        }
        DeleteMaterial(material);
    }

    // BORRAR material
    public void DeleteMaterial(MaterialObject material)
    {
        // lo saco de la lista
        materials.Remove(material);
        selectionManager.RemoveObject(material.MaterialIndex);
        // lo destruyo
        Destroy(material);
        // disponibilizo la posición de nuevo
        positionManager.AvailablePositions[material.MaterialIndex] = true;
        Debug.Log("[MaterialManager] :: Se ha borrado el material " + material.MaterialIndex);
    }

    // Borrar material por indice
    public void DeleteMaterial(int materialIndex)
    {
        MaterialObject material = FindMaterialInList(materialIndex);
        // lo saco de la lista
        materials.Remove(material);
        selectionManager.RemoveObject(materialIndex);
        // lo destruyo
        Destroy(material);
        // disponibilizo la posición de nuevo
        positionManager.AvailablePositions[materialIndex] = true;

        //no va popup
        Debug.Log("[MaterialManager] :: Se ha borrado el material " + materialIndex);
    }

    public List<int> GetSelectedMaterials()
    {
        List<int> selectedMaterials = new List<int>();
        List<int> selectedObjects = selectionManager.SelectedObjects;
        foreach (int index in selectedObjects)
        {
            MaterialObject materials = FindMaterialInList(index);
            if (materials != null)
            {
                selectedMaterials.Add(index);
            }
        }
        return selectedMaterials;
    }

    /**
    * Elimina todas los materiales
    */
    public void DeleteAllMaterials()
    {
        List<MaterialObject> toDelete = new List<MaterialObject>(materials);

        foreach (MaterialObject material in toDelete)
        {
            DeleteMaterial(material);
        }
        //el panel de las subparticulas se tiene que ocultar
        selectionManager.PanelElements.GetComponent<CanvasGroup>().alpha = 0;
    }
}
