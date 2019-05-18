using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject[] nucleonPrefabs;
    [SerializeField]
    private Transform parent;

    public void SpawnNucleon(bool proton)
    {
        int index = 1;
        if (proton)
        {
            index = 0;
        }
        GameObject prefab = nucleonPrefabs[index];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
    }

    public void SpawnElectron()
    {
        GameObject prefab = nucleonPrefabs[2];
        GameObject spawn = Instantiate<GameObject>(prefab, parent);
    }
}
