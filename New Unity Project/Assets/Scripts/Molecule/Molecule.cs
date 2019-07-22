using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    //lista de átomos
    private List<GameObject> atoms = new List<GameObject>();
    //lista con data de átomos
    private List<AtomInMolPositionData> atomsData = new List<AtomInMolPositionData>();
    //prefabs y parent
    public GameObject atomPrefab;
    public GameObject connectionPrefab;
    public Transform parent;
    //label que indica nombre de molécula
    public GameObject moleculeLabel;

    //spawnear un átomo 
    public void SpawnAtom(AtomInMolPositionData positionData, Material mat)
    {
        //obtengo la posición de la data
        Vector3 position = new Vector3(positionData.XPos, positionData.YPos, positionData.ZPos);
        //lo creo
        GameObject spawn = Instantiate<GameObject>(atomPrefab, parent);
        //seteo posición
        spawn.transform.localPosition = position;
        //seteo tamaño
        Vector3 scale = new Vector3(positionData.Scale, positionData.Scale, positionData.Scale);
        spawn.transform.localScale = scale;
        //seteo material
        spawn.GetComponent<Renderer>().material = mat;
        //lo agrego a las listas
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

    //spawnea una conexión entre dos átomos
    //type => 1-> simple, 2-> doble, 3-> triple
    public void SpawnConnection(int from, int to, int type)
    {
        //obtengo el índice de los átomos de acuerdo al índice de la data
        int indexAtomFrom = atomsData.FindIndex(d => d.Id.Equals(from));
        int indexAtomTo = atomsData.FindIndex(d => d.Id.Equals(to));
        GameObject atomFrom = atoms[indexAtomFrom];
        GameObject atomTo = atoms[indexAtomTo];
        //posición
        Vector3 positionFrom = atomFrom.transform.localPosition;
        Vector3 positionTo = atomTo.transform.localPosition;
        SpawnConnection(positionFrom, positionTo);
        //si es mayor a uno significa que necesito agregar 1 a 0.025 + en X
        if (type > 1)
        {
            positionFrom.x += 0.025f;
            positionTo.x += 0.025f;
            SpawnConnection(positionFrom, positionTo);
        }
        //y si es igual a 3 significa que agrego la anterior y una mas a 0.025 - en X
        //-0.05 porque ya se movió 0.025 a la derecha y ahora se tiene q mover el doble a la izq
        if (type.Equals(3))
        {
            positionFrom.x -= 0.05f;
            positionTo.x -= 0.05f;
            SpawnConnection(positionFrom, positionTo);
        }
    }

    private void SpawnConnection(Vector3 positionFrom, Vector3 positionTo)
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
        newConnection.transform.localScale = new Vector3(0.01f, distance / 2, 0.01f);
    }

    public void SetMoleculeName(string name)
    {
        moleculeLabel.GetComponent<TextMesh>().text = name;
    }
}
