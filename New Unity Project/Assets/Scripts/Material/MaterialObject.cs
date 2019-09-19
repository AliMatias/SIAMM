using UnityEngine;
using UnityEditor;

public class MaterialObject : MonoBehaviour
{
    public Transform parent;
    public GameObject materialLabel;
    private int materialIndex;

    public int MaterialIndex { get => materialIndex; set => materialIndex = value; }

    public void SetMaterialName(string name)
    {
        materialLabel.GetComponent<TextMesh>().text = name;
    }

    public void SpawnModel(Object model)
    {
        GameObject materialModel = Instantiate(model, parent) as GameObject;
    }
}