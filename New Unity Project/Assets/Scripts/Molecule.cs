using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    private List<GameObject> atoms = new List<GameObject>();
    private List<AtomInMolPositionData> atomsData = new List<AtomInMolPositionData>();
    public GameObject atomPrefab;
    public GameObject connectionPrefab;
    public Transform parent;



    public void SpawnAtom(AtomInMolPositionData positionData, Material mat)
    {
        Vector3 position = new Vector3(positionData.XPos, positionData.YPos, positionData.ZPos);
        GameObject spawn = Instantiate<GameObject>(atomPrefab, parent);
        spawn.transform.localPosition = position;
        Vector3 scale = new Vector3(positionData.Scale, positionData.Scale, positionData.Scale);
        spawn.transform.localScale = scale;
        spawn.GetComponent<Renderer>().material = mat;
        atoms.Add(spawn);
        atomsData.Add(positionData);
    }

    private void FixedUpdate()
    {
        //rota el objeto 0.1 grados en el eje Y
        parent.Rotate(0, 0.1f, 0);
    }

    public void SpawnConnection(int from, int to)
    {
        int indexAtomFrom = atomsData.FindIndex(d => d.Id.Equals(from));
        int indexAtomTo = atomsData.FindIndex(d => d.Id.Equals(to));
        GameObject atomFrom = atoms[indexAtomFrom];
        GameObject atomTo = atoms[indexAtomTo];
        Vector3 positionFrom = atomFrom.transform.localPosition;
        Vector3 positionTo = atomTo.transform.localPosition;
        GameObject newConnection = Instantiate<GameObject>(connectionPrefab, parent);
        //position
        newConnection.transform.localPosition = (positionFrom + positionTo) / 2.0f;
        // Rotation
        Vector3 direction = Vector3.Normalize(positionTo - positionFrom);
        Vector3 defaultOrientation = new Vector3(0, 1, 0);
        Vector3 rotation = Vector3.Normalize(direction + defaultOrientation);
        newConnection.transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, 0);
        //scale
        float distance = Vector3.Distance(atomFrom.transform.localPosition, atomTo.transform.localPosition);
        newConnection.transform.localScale = new Vector3(0.01f, distance/2, 0.01f);

    }
}
