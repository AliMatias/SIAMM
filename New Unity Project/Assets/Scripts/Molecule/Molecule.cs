using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    private MoleculeManager moleculeManager;
    //lista de átomos
    private List<GameObject> atoms = new List<GameObject>();
    //lista de conexiones
    private List<GameObject> connections = new List<GameObject>();
    //lista con data de átomos
    private List<AtomInMolPositionData> atomsData = new List<AtomInMolPositionData>();
    //prefabs y parent
    public GameObject atomPrefab;
    public GameObject connectionPrefab;
    public Transform parent;
    //label que indica nombre de molécula
    public GameObject moleculeLabel;
    //indice que indica posición+
    private int moleculeIndex;
    //indica id en tabla de moléculas
    private int moleculeId;

    public int MoleculeId { get => moleculeId; set => moleculeId = value; }
    public int MoleculeIndex { get => moleculeIndex; set => moleculeIndex = value; }

    private void Awake()
    {
        moleculeManager = FindObjectOfType<MoleculeManager>();
    }

    // spawnear un átomo 
    public void SpawnAtom(AtomInMolPositionData positionData, Material mat)
    {
        // obtengo la posición de la data
        Vector3 position = new Vector3(positionData.XPos, positionData.YPos, positionData.ZPos);
        // creo una copia del prefab
        GameObject tempPrefab = Instantiate<GameObject>(atomPrefab);
        
        // seteo el material 
        // (necesario setear el material antes de instanciar el objeto
        //  para que el highlight tome el normalColor correcto)
        tempPrefab.GetComponent<Renderer>().material = mat;
        
        // seteo posición
        tempPrefab.transform.localPosition = position;
        // seteo tamaño
        Vector3 scale = new Vector3(positionData.Scale, positionData.Scale, positionData.Scale);
        tempPrefab.transform.localScale = scale;

        // lo creo y borro la copia del prefab
        GameObject spawn = Instantiate<GameObject>(tempPrefab, parent);
        Destroy(tempPrefab);
        // lo agrego a las listas
        atoms.Add(spawn);
        atomsData.Add(positionData);
    }

    //rotar la molécula y el label
    private void FixedUpdate()
    {
        //rota el objeto 0.15 grados en el eje Y a la derecha
        parent.Rotate(0, 0.15f, 0);
        //y el label al revés
        moleculeLabel.transform.Rotate(0, -0.15f, 0);
    }

    /*  cuando se destruye la instancia de este script, tengo que destruir
    *   manualmente el gameObject al cual está asignado este script
    */
    void OnDestroy()
    {
        Destroy(gameObject);
    }

    //spawnea una conexión entre dos átomos
    //type => 1-> simple, 2-> doble, 3-> triple
    public void SpawnConnection(int from, int to, int type, int lineType)
    {
        //obtengo el índice de los átomos de acuerdo al índice de la data
        int indexAtomFrom = atomsData.FindIndex(d => d.Id.Equals(from));
        int indexAtomTo = atomsData.FindIndex(d => d.Id.Equals(to));
        GameObject atomFrom = atoms[indexAtomFrom];
        GameObject atomTo = atoms[indexAtomTo];
        //posición
        Vector3 positionFrom = atomFrom.transform.localPosition;
        Vector3 positionTo = atomTo.transform.localPosition;

        if (type.Equals(1) || type.Equals(3))
            SpawnConnection(positionFrom, positionTo, lineType);
        
        //si es mayor a uno significa que necesito agregar 1 a 0.025 + en todos los ejes, porque depende de las coordenadas en donde se ubican los atomos
        if (type.Equals(2) || type.Equals(3))
        {
            //spawn de la 1ra linea pero tiene que tener proporcion en la posicion... sino queda desfazado por eso no spawnea antes
            positionFrom.x += 0.025f;
            positionTo.x += 0.025f;
            positionFrom.y += 0.025f;
            positionTo.y += 0.025f;
            positionFrom.z += 0.025f;
            positionTo.z += 0.025f;
            SpawnConnection(positionFrom, positionTo, lineType);

            //y si es igual a 3 significa que agrego la anterior y una mas a 0.025 - en todos los ejes, porque depende de las coordenadas en donde se ubican los atomos
            //-0.05 porque ya se movió 0.025 a la derecha y ahora se tiene q mover el doble a la izq
            positionFrom.x -= 0.05f;
            positionTo.x -= 0.05f;
            positionFrom.y -= 0.05f;
            positionTo.y -= 0.05f;
            positionFrom.z -= 0.05f;
            positionTo.z -= 0.05f;
            SpawnConnection(positionFrom, positionTo, lineType);
        }    
    }

    private void SpawnConnection(Vector3 positionFrom, Vector3 positionTo, int lineType)
    {
        GameObject newConnection = Instantiate<GameObject>(connectionPrefab, parent);
        newConnection.transform.localPosition = (positionFrom + positionTo) / 2.0f;
        //rotación
        Vector3 direction = Vector3.Normalize(positionTo - positionFrom);
        Vector3 defaultOrientation = new Vector3(0, 1, 0);
        Vector3 rotation = Vector3.Normalize(direction + defaultOrientation);
        newConnection.transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, 0);
        //tamaño
        float distance = Vector3.Distance(positionFrom, positionTo);

        if (lineType == 2)
        {
            // ESTA sera para la UNIONICA (la que no tiene coneccion y quedamos con el profesor de mostrarla finita)
            newConnection.transform.localScale = new Vector3(0.01f, distance / 2, 0.01f);    
        }
        else // lineType == 1 normal y el 0 quedo para el atomo que no tiene conexiones segun nuestra logica
        {
            // no importa la escala del prefab aca setea
            newConnection.transform.localScale = new Vector3(0.04f, distance / 2, 0.04f);
        }
        connections.Add(newConnection);
    }

    public void SetMoleculeName(string name)
    {
        moleculeLabel.GetComponent<TextMesh>().text = name;
    }
    
    //se lanza cuando se hace click a la molécula
    public void OnMouseDown()
    {
        Debug.Log("clickeaste la molécula " + MoleculeIndex);
        moleculeManager.SelectMolecule(MoleculeIndex);
    }

    public void Select()
    {
        moleculeLabel.GetComponent<TextMesh>().color = new Color(240, 0, 0);
        StartAllHighlights(atoms);
        StartAllHighlights(connections);
    }

    public void Deselect()
    {
        moleculeLabel.GetComponent<TextMesh>().color = new Color(255, 255, 255);
        StopAllHighlights(atoms);
        StopAllHighlights(connections);
    }

    private void StartAllHighlights(List<GameObject> list)
    {
        foreach(GameObject obj in list)
        {
            obj.GetComponent<HighlightObject>().StartHighlight();
        }
    }

    private void StopAllHighlights(List<GameObject> list)
    {
        foreach (GameObject obj in list)
        {
            obj.GetComponent<HighlightObject>().StopHighlight();
        }
    }
}
