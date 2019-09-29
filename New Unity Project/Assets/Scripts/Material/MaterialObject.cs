using UnityEngine;
using UnityEditor;

public class MaterialObject : MonoBehaviour
{
    public Transform parent;
    public GameObject materialLabel;
    private GameObject materialModel;
    private MaterialManager materialManager;
    //position
    private int materialIndex;
    //table id
    private int materialId;
    private string materialName;
    private string modelFile;

    public int MaterialIndex { get => materialIndex; set => materialIndex = value; }
    public int MaterialId { get => materialId; set => materialId = value; }
    public string MaterialName { get => materialName; set => materialName = value; }
    public string ModelFile { get => modelFile; set => modelFile = value; }

    private void Awake()
    {
        materialManager = FindObjectOfType<MaterialManager>();
    }

    //rotar el material y el label
    private void FixedUpdate()
    {
        //rota el objeto 0.15 grados en el eje Y a la derecha
        parent.Rotate(0, 0.15f, 0);
        //y el label al revés
        materialLabel.transform.Rotate(0, -0.15f, 0);
    }

    /*  cuando se destruye la instancia de este script, tengo que destruir
    *   manualmente el gameObject al cual está asignado este script
    */
    void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        materialManager.SelectMaterial(materialIndex);
    }

    public void SetMaterialName(string name)
    {
        materialLabel.GetComponent<TextMesh>().text = name;
        materialName = name;
    }

    public void SpawnModel(Object model)
    {
        materialModel = Instantiate(model, parent) as GameObject;
    }

    public void Select()
    {
        materialLabel.GetComponent<TextMesh>().color = new Color(240, 0, 0);
        // StartAllHighlights();
    }

    public void Deselect()
    {
        materialLabel.GetComponent<TextMesh>().color = new Color(255, 255, 255);

        // El stop no esta funcionando
        // StopAllHighlights();
    }

    private void StartAllHighlights()
    {
        materialModel.GetComponent<HighlightObject>().StartHighlight();
    }

    private void StopAllHighlights()
    {
        materialModel.GetComponent<HighlightObject>().StopHighlight();
    }
}