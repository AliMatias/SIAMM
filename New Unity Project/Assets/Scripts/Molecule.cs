using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    private List<GameObject> atoms = new List<GameObject>();
    public GameObject atomPrefab;
    public Transform parent;


    public void SpawnAtom(AtomInMolPositionData positionData, Material mat)
    {
        Vector3 position = new Vector3(positionData.XPos, positionData.YPos, positionData.ZPos);
        GameObject spawn = Instantiate<GameObject>(atomPrefab, parent);
        spawn.transform.localPosition = position;
        Vector3 scale = new Vector3(positionData.Scale, positionData.Scale, positionData.Scale);
        spawn.transform.localScale = scale;
        spawn.GetComponent<Renderer>().material = mat;
    }

    private void FixedUpdate()
    {
        //rota el objeto 0.1 grados en el eje Y
        parent.Rotate(0, 0.1f, 0);
    }
}
