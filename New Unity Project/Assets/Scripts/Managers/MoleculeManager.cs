using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoleculeManager : MonoBehaviour
{
    private PositionManager positionManager = PositionManager.Instance;
    private SelectionManager selectionManager;
    private QryElementos qryElement;
    //prefab de molécula
    public Molecule moleculePrefab;
    //lista de moléculas
    private List<Molecule> molecules = new List<Molecule>();

    private List<Button> moleculeButtons = new List<Button>();
    [SerializeField]
    private Button addMoleculeButton;
    //En este array estan todos los materiales, se asignan desde la interfaz
    public Material[] materials;
    //diccionario que mapea categoría de la tabla, con posición en array de materiales
    private Dictionary<string, int> categories = new Dictionary<string, int>();
    private UIPopup popup;

    public List<Molecule> Molecules { get => molecules; }

    private void Awake()
    {
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("moleculeToggle");
        foreach (GameObject btn in buttons)
        {
            moleculeButtons.Add(btn.GetComponent<Button>());
        }
        activateDeactivateMoleculeButtons();

        popup = FindObjectOfType<UIPopup>();
        selectionManager = FindObjectOfType<SelectionManager>();
        IntializeCategoryDictionary();
    }

    //activa-desactiva botones de acuerdo a la cant de moleculas
    private void activateDeactivateMoleculeButtons()
    {
        bool status = true;
        if (molecules.Count == 0)
        {
            status = false;
        }

        foreach (Button btn in moleculeButtons)
        {
            btn.interactable = status;
        }

        if (positionManager.NoPositionsLeft())
        {
            addMoleculeButton.interactable = false;
        }
        else
        {
            addMoleculeButton.interactable = true;
        }
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
        newMolecule.transform.localPosition = positionManager.Positions[position];
        newMolecule.MoleculeIndex = position;
        molecules.Add(newMolecule);
        //seteo nombre
        newMolecule.SetMoleculeName(name);
        List<AtomInMolPositionData> normalizedAtoms = NormalizeAtomPositions(atomsPosition);
        //spawneo todos sus átomos
        foreach (AtomInMolPositionData pos in normalizedAtoms)
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
        foreach(AtomInMolPositionData atom in normalizedAtoms)
        {
            //si es que tiene alguna
            if(atom.ConnectedTo > 0)
            {
                newMolecule.SpawnConnection(atom.Id, atom.ConnectedTo, atom.ConnectionType, atom.LineType);//por ej aca 1 seria comun 2 podria ser unionica
            }
        }
        activateDeactivateMoleculeButtons();
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
        categories.Add("Actinidos", 10);
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

    //seleccionar molécula
    public void SelectMolecule(int index)
    {
        Molecule selectedMolecule = FindMoleculeInList(index);
        selectionManager.SelectObject(selectedMolecule);
    }

    // BORRAR molécula seleccionada.
    public void DeleteSelectedMolecule()
    {
        List<int> selectedObjects = selectionManager.SelectedObjects;
        Molecule molecule = selectedObjects.Count > 0 ?
            FindMoleculeInList(selectedObjects.Last()) :
            null;

        if (molecule == null)
        {
            Debug.Log("[MoleculeManager] :: No hay ningúna molécula seleccionada");
            popup.MostrarPopUp("Error", "No hay ningúna molécula seleccionada");

            return;
        }
        DeleteMolecule(molecule);
    }

    // BORRAR molécula
    public void DeleteMolecule(Molecule molecule)
    {
        Debug.Log(molecule.MoleculeIndex);
        // la saco de la lista
        molecules.Remove(molecule);
        selectionManager.RemoveObject(molecule.MoleculeIndex);
        // la destruyo
        Destroy(molecule);
        // disponibilizo la posición de nuevo
        positionManager.AvailablePositions[molecule.MoleculeIndex] = true;

        //no va popup
        Debug.Log("[MoleculeManager] :: Se ha borrado la molécula " + molecule.MoleculeIndex);

        activateDeactivateMoleculeButtons();
    }

    public List<int> GetSelectedMolecules()
    {
        List<int> selectedMolecules = new List<int>();
        List<int> selectedObjects = selectionManager.SelectedObjects;
        foreach (int index in selectedObjects)
        {
            Molecule molecule = FindMoleculeInList(index);
            if (molecule != null)
            {
                selectedMolecules.Add(index);
            }
        }
        return selectedMolecules;
    }

    /**
     * Reduce las posiciones de los átomos de una molécula a un valor entre -1 y 1 si es necesario.
     * Devuelve una lista de atomos normalizada, mantiene inmutabilidad.
     */
    public List<AtomInMolPositionData> NormalizeAtomPositions(List<AtomInMolPositionData> atoms)
    {
        List<AtomInMolPositionData> normalizedAtoms = new List<AtomInMolPositionData>();
        List<float> positions = new List<float>();
        // obtengo el mayor valor absoluto de las posiciones
        foreach (AtomInMolPositionData atom in atoms)
        {
            positions.Add(Math.Abs(atom.XPos));
            positions.Add(Math.Abs(atom.YPos));
            positions.Add(Math.Abs(atom.ZPos));
        }
        float maxValue = positions.Max();
        // si el mayor no supera a 1 no hay que hacer nada
        if(maxValue <= 1)
        {
            return atoms;
        }

        // si es mayor a uno, divido todas las posiciones por el mayor
        foreach (AtomInMolPositionData atom in atoms)
        {
            AtomInMolPositionData normalizedAtom = new AtomInMolPositionData(atom);
            normalizedAtom.XPos /= maxValue;
            normalizedAtom.YPos /= maxValue;
            normalizedAtom.ZPos /= maxValue;
            normalizedAtoms.Add(normalizedAtom);
        }

        return normalizedAtoms;
    }
}
