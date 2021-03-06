﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoleculeManager : MonoBehaviour
{
    #region Atributos
    private PositionManager positionManager = PositionManager.Instance;
    private SelectionManager selectionManager;
    private SuggestionManager suggestionManager;
    private TipsManager tipsManager;
    private QryElementos qryElement;
    //prefab de molécula
    public Molecule moleculePrefab;
    //lista de moléculas
    private List<Molecule> molecules = new List<Molecule>();

    private List<Button> moleculeButtons = new List<Button>();
    [SerializeField]
    private Button addMoleculeButton;
    private UIPopup popup;

    public List<Molecule> Molecules { get => molecules; }

    //panel de info
    private MainInfoPanel mainInfoPanel;

    private CombinationManager combinationManager;

    #endregion

    private void Awake()
    {
        //instancio en el momento la clase que contiene las querys, seria lo mismo que hacer class algo = new class();
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        suggestionManager = FindObjectOfType<SuggestionManager>();
        tipsManager = FindObjectOfType<TipsManager>();

        //GameObject[] buttons = GameObject.FindGameObjectsWithTag("moleculeToggle");
        //foreach (GameObject btn in buttons)
        //{
        //    moleculeButtons.Add(btn.GetComponent<Button>());
        //}
        activateDeactivateMoleculeButtons();

        popup = FindObjectOfType<UIPopup>();
        selectionManager = FindObjectOfType<SelectionManager>();

        mainInfoPanel = FindObjectOfType<MainInfoPanel>();

        combinationManager = FindObjectOfType<CombinationManager>();
    }

    //activa-desactiva botones de acuerdo a la cant de moleculas
    private void activateDeactivateMoleculeButtons()
    {
        //bool status = true;
        //if (molecules.Count == 0)
        //{
        //    status = false;
        //}

        //foreach (Button btn in moleculeButtons)
        //{
        //    btn.interactable = status;
        //}

        if (positionManager.NoPositionsLeft())//si no hay mas posiciones disponibles en la cuadricula DESACTIVA EL BOTON!
        {
            addMoleculeButton.interactable = false;
        }
        else
        {
            //aca deberia controlar por las dudas que no este en modo combinacion.. para que no active el boton..
            if (combinationManager != null && combinationManager.CombineMode == false)
            {
                addMoleculeButton.interactable = true;
            }
        }
    }


    #region Metodos SPAWN MOLECULAS 
    //retorna una estructura que tendra la posicion en la lista de lugares tomados y la molecula para saber luego su posicion XYZ
    public Molecule GetMoleculePos(bool fromCombination)
    {
        //intento obtener una posición disponible
        int position;
        try
        {
            position = positionManager.GetFirstAvailablePositionIndex();
        }
        //si no hay mas posiciones disponibles, lo loggeo y me voy
        catch (NoPositionsLeftException nple)
        {
            //no va popup -> preguntar si no se deberia mostrar mensaje
            Debug.Log(nple.Message);
            throw;
        }

        //instancio la molécula, y seteo posición
        Molecule newMolecule = Instantiate<Molecule>(moleculePrefab);
        //aca le digo que ira la molecula armada en esta posicion final
        newMolecule.transform.localPosition = positionManager.Positions[position];
        newMolecule.MoleculeIndex = position;

        if (fromCombination)
        {
            //LA MOLECULA COMIENZA NO ACTIVA PARA QUE NO SE VEA! solo cuando se hace la combinacion de elementos
            newMolecule.gameObject.SetActive(false);
        }

        molecules.Add(newMolecule);

        return newMolecule;
    }


    //spawnear molécula (objeto vacío donde se meten los objetos, como "Atom") METODO PARA SPAWN CENTRAL
    public void SpawnMolecule(List<AtomInMolPositionData> atomsPosition, string name, Molecule newMolecule)
    {
        //seteo nombre
        newMolecule.SetMoleculeName(name);
        List<AtomInMolPositionData> normalizedAtoms = NormalizeAtomPositions(atomsPosition);
        //seteo su ID de molécula
        newMolecule.MoleculeId = normalizedAtoms[0].MoleculeId;
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

            //obtengo el color del elemento.
            Color32 color = qryElement.GetElementColor(pos.ElementId);
            newMolecule.SpawnAtom(pos, color);
        }

        //y despues sus conexiones una vez que esten todos posicionados
        foreach (AtomInMolPositionData atom in normalizedAtoms)
        {
            //si es que tiene alguna
            if (atom.ConnectedTo > 0)
            {
                newMolecule.SpawnConnection(atom.Id, atom.ConnectedTo, atom.ConnectionType, atom.LineType);//por ej aca 1 seria comun 2 podria ser unionica
            }
        }


        //dejo seleccionada la molecula nueva!
        SelectMolecule(newMolecule.MoleculeIndex);

        //cargo info en panel inferior!
        mainInfoPanel.SetInfoMolecule(newMolecule);

        //interacion sobre los botones de la UI
        activateDeactivateMoleculeButtons();

        // actualizo panel de sugerencias
        suggestionManager.updateSuggestions();

        /*CREA UN TIP! CON LA TEMATICA PASADA POR ID*/
        tipsManager.LaunchTips(3);
    }

    //spawnear molécula (objeto vacío donde se meten los objetos, como "Atom") METODO PARA SPAWN DESDE LISTA 
    public void SpawnMolecule(List<AtomInMolPositionData> atomsPosition, string name)
    {
        //si este metodo es llamado quiere decir que la molecula viene de la lista y se tiene que instanciar en el manager al molecula
        Molecule newMolecule = GetMoleculePos(false);
        SpawnMolecule(atomsPosition, name, newMolecule);
    }

    public void SpawnMoleculeFromSavedData(List<AtomInMolPositionData> atomsPosition, string name, int position, int moleculeId){
        if(positionManager.OccupyPosition(position)){
            //instancio la molécula, y seteo posición
            Molecule newMolecule = Instantiate<Molecule>(moleculePrefab);
            //aca le digo que ira la molecula armada en esta posicion final
            newMolecule.transform.localPosition = positionManager.Positions[position];
            newMolecule.MoleculeIndex = position;
            molecules.Add(newMolecule);
            SpawnMolecule(atomsPosition, name, newMolecule);
        }else{
            Debug.LogError("Molécula con id " + moleculeId + "en posición " + position + " no cargada.");
        }
    }


    #endregion

    #region ActionOnMolecules

    public Molecule FindMoleculeInList(int index)
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
        suggestionManager.updateSuggestions();
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
        // la saco de la lista
        molecules.Remove(molecule);
        selectionManager.RemoveObject(molecule.MoleculeIndex);
        // la destruyo
        Destroy(molecule);
        // disponibilizo la posición de nuevo
        positionManager.AvailablePositions[molecule.MoleculeIndex] = true;

        //no va popup
        Debug.Log("[MoleculeManager] :: Se ha borrado la molécula " + molecule.MoleculeIndex);

        suggestionManager.updateSuggestions();
        activateDeactivateMoleculeButtons();
    }

    // Borrar molecula por indice
    public void DeleteMolecule(int moleculeIndex)
    {
        Molecule molecule = FindMoleculeInList(moleculeIndex);
        DeleteMolecule(molecule);
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

    public List<int> GetSelectedMoleculeIds()
    {
        List<int> selectedMolecules = new List<int>();
        List<int> selectedObjects = selectionManager.SelectedObjects;
        foreach (int index in selectedObjects)
        {
            Molecule molecule = FindMoleculeInList(index);
            if (molecule != null)
            {
                selectedMolecules.Add(molecule.MoleculeId);
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

    /**
     * Elimina todas las moleculas
     */
     public void DeleteAllMolecules()
    {
        List<Molecule> toDelete = new List<Molecule>(molecules);

        foreach (Molecule molecule in toDelete)
        {
            DeleteMolecule(molecule);
        }
        //el panel de las subparticulas se tiene que ocultar
        selectionManager.PanelElements.GetComponent<CanvasGroup>().alpha = 0;
    }
    #endregion
}
