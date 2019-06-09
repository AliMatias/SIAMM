using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour
{
    //prefab asignado por interfaz
    [SerializeField]
    private Atom atomPrefab;
    //lista que maneja los átomos en pantalla
    private List<Atom> atomsList = new List<Atom>();
    
    //agregar nuevo átomo al espacio de trabajo
    public void NewAtom()
    {
        //instancio
        Atom spawnedAtom = Instantiate<Atom>(atomPrefab);
        //agrego a la lista
        atomsList.Add(spawnedAtom);
        //spawneo un protón
        spawnedAtom.SpawnNucleon(true);
        
    }

}
