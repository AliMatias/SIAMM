using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject[] nucleonPrefabs;
    [SerializeField]
    private Transform parent;
    private Queue<GameObject> protonQueue = new Queue<GameObject>();
    private Queue<GameObject> neutronQueue = new Queue<GameObject>();
    private Queue<GameObject> electronQueue = new Queue<GameObject>();
    private int protonCounter = 0;
    private int neutronCounter = 0;
    private int electronCounter = 0;

    public void SpawnNucleon(bool proton)
    {
        int index = 1;
        if (proton)
        {
            index = 0;
        }
        GameObject prefab = nucleonPrefabs[index];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        if (proton)
        {
            protonQueue.Enqueue(spawn);
            protonCounter++;
        }
        else
        {
            neutronQueue.Enqueue(spawn);
            neutronCounter++;
        }
    }

    public void SpawnElectron()
    {
        GameObject prefab = nucleonPrefabs[2];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
        electronQueue.Enqueue(spawn);
        electronCounter++;
    }

    public void RemoveNeutron()
    {
        if (neutronCounter != 0)
        {
            GameObject toDelete = neutronQueue.Dequeue();
            Destroy(toDelete);
            neutronCounter--;
        }
    }

    public void RemoveProton()
    {
        if (protonCounter != 0)
        {
            GameObject toDelete = protonQueue.Dequeue();
            Destroy(toDelete);
            protonCounter--;
        }
    }

    public void RemoveElectron()
    {
        if (electronCounter != 0)
        {
            GameObject toDelete = electronQueue.Dequeue();
            Destroy(toDelete);
            electronCounter--;
        }
    }
}
