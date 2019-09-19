using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialManager : MonoBehaviour
{
    private PositionManager positionManager = PositionManager.Instance;

    public MaterialObject materialPrefab;

    private List<MaterialObject> materials;

    public void Awake()
    {
        materials = new List<MaterialObject>();
    }

    public void SpawnMaterial(MaterialData materialData)
    {
        MaterialObject newMaterial = GetMaterialPos();

        newMaterial.SetMaterialName(materialData.Name);
        
        newMaterial.SpawnModel(Resources.Load("MaterialModels/" + materialData.ModelFile, typeof(GameObject)));
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
}
