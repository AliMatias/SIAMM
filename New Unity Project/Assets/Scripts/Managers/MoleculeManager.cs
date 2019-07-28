using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoleculeManager : MonoBehaviour
{
    private PositionManager positionManager = PositionManager.Instance;
    private QryElementos qryElement;
    //prefab de molécula
    public Molecule moleculePrefab;
    //lista de moléculas
    private List<Molecule> molecules = new List<Molecule>();
    //En este array estan todos los materiales, se asignan desde la interfaz
    public Material[] materials;
    //diccionario que mapea categoría de la tabla, con posición en array de materiales
    private Dictionary<string, int> categories = new Dictionary<string, int>();
    private UIPopup popup;

    private void Awake()
    {
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        popup = FindObjectOfType<UIPopup>();
        IntializeCategoryDictionary();
    }

    //spawnear molécula (objeto vacío donde se meten los objetos, como "Atom")
    public void SpawnMolecule(List<AtomInMolPositionData> atomsPosition, string name)
    {
        //intento obtener una posición disponible random
        int position;
        try
        {
            position = positionManager.ObtainRandomPositionIndex();
        }
        //si no hay mas posiciones disponibles, lo loggeo y me voy
        catch (NoPositionsLeftException nple)
        {
            //no va popup -> preguntar si no se deberia mostrar mensaje
            Debug.Log(nple.Message);
            return;
        }
        //instancio la molécula, y seteo posición
        Molecule newMolecule = Instantiate<Molecule>(moleculePrefab);
        newMolecule.transform.localPosition = positionManager.PlanePositions[position];
        newMolecule.MoleculeIndex = position;
        molecules.Add(newMolecule);
        //seteo nombre
        newMolecule.SetMoleculeName(name);
        //spawneo todos sus átomos
        foreach (AtomInMolPositionData pos in atomsPosition)
        {
            //query a la tabla de elementos para obtener clasificación_grupo
            ElementTabPer element = new ElementTabPer();
            try
            {
                element = qryElement.GetElementFromNro(pos.ElementId);
            }
            catch (Exception e)
            {
                Debug.LogError("MoleculeManager :: Ocurrio un error al buscar Elemento desde Identificador: " + e.Message);
                popup.MostrarPopUp("Elementos Qry DB", "Error obteniendo Elemento desde Identificador");
                return;
            }

            //obtengo el material según la clasif
            Material mat = materials[GetMaterialIndexFromDictionary(element.ClasificacionGrupo)];
            newMolecule.SpawnAtom(pos, mat);
        }
        //y despues sus conexiones una vez que esten todos posicionados
        foreach(AtomInMolPositionData atom in atomsPosition)
        {
            //si es que tiene alguna
            if(atom.ConnectedTo > 0)
            {
                newMolecule.SpawnConnection(atom.Id, atom.ConnectedTo, atom.ConnectionType, atom.LineType);//por ej aca 1 seria comun 2 podria ser unionica
            }
        }
    }

    //diccionario de categoría_grupo -> material
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

    //método para obtener el material
    private int GetMaterialIndexFromDictionary(string cat)
    {
        return categories[cat];
    }

    private Molecule FindMoleculeInList(int index)
    {
        foreach(Molecule molecule in molecules)
        {
            if(molecule.MoleculeIndex == index)
            {
                return molecule;
            }
        }
        return null;
    }
}
