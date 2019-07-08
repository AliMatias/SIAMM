using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeManager : MonoBehaviour
{
    private AtomManager atomManager;
    private DBManager dBManager;
    //prefab de molécula
    public Molecule moleculePrefab;
    //lista de moléculas
    private List<Molecule> molecules = new List<Molecule>();
    //En este array estan todos los materiales, se asignan desde la interfaz
    public Material[] materials;
    //diccionario que mapea categoría de la tabla, con posición en array de materiales
    private Dictionary<string, int> categories = new Dictionary<string, int>();

    private void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        dBManager = FindObjectOfType<DBManager>();
        IntializeCategoryDictionary();
    }

    public void SpawnMolecule(List<AtomInMolPositionData> atomsPosition)
    {
        //intento obtener una posición disponible random
        int position;
        try
        {
            position = atomManager.ObtainRandomPositionIndex();
        }
        //si no hay mas posiciones disponibles, lo loggeo y me voy
        catch (NoPositionsLeftException nple)
        {
            Debug.Log(nple.Message);
            return;
        }
        Molecule newMolecule = Instantiate<Molecule>(moleculePrefab);
        newMolecule.transform.localPosition = atomManager.PlanePositions[position];
        molecules.Add(newMolecule);
        //spawneo todos los átomos
        foreach (AtomInMolPositionData pos in atomsPosition)
        {
            ElementTabPer element = dBManager.GetElementFromNro(pos.ElementId);
            Material mat = materials[GetMaterialIndexFromDictionary(element.ClasificacionGrupo)];
            newMolecule.SpawnAtom(pos, mat);
        }
        //y despues sus conexiones una vez que esten todos posicionados
        foreach(AtomInMolPositionData atom in atomsPosition)
        {
            if(atom.ConnectedTo > 0)
            {
                newMolecule.SpawnConnection(atom.Id, atom.ConnectedTo);
            }
        }
    }

    private void IntializeCategoryDictionary()
    {
        categories.Add("Sin Grupo", 0);
        categories.Add("Gas Inerte", 1);
        categories.Add("Alcalino", 2);
        categories.Add("Alcalino Terreo", 3);
        categories.Add("Metaloide", 4);
        categories.Add("No Metal", 5);
        categories.Add("Halogeno", 6);
        categories.Add("Pobre", 7);
        categories.Add("De Transicion", 8);
        categories.Add("Lantanido", 9);
        categories.Add("Actinido", 10);
    }

    private int GetMaterialIndexFromDictionary(string cat)
    {
        return categories[cat];
    }
}
