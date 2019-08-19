using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private CombinationManager combinationManager;
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private List<int> selectedObjects;
    //se asigna desde la interfaz
    public CanvasGroup panelElements;

    //hace referencia desde la clase selection manager
    public List<int> SelectedObjects { get => selectedObjects; }


    private void Awake()
    {
        selectedObjects = new List<int>();
        combinationManager = FindObjectOfType<CombinationManager>();
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
    }

    public bool SelectObject(Atom atom)
    {
        // verifico si el objeto estaba seleccionado
        if (selectedObjects.IndexOf(atom.AtomIndex) != -1)
        {
            // Este átomo ya estaba seleccionado. Se quitará la selección
            selectedObjects.Remove(atom.AtomIndex);
            atom.Deselect();
            //no muestro panel de agregar elementos
            panelElements.alpha = 0;
            return false;
        }

        // si habia uno seleccionado, lo deselecciono
        if (!combinationManager.CombineMode)
        {
            DeselectAll();
        }

        selectedObjects.Add(atom.AtomIndex);
        atom.Select();

        //muestro ademas el panel de agregar elementos! 
        //SI EL MODO ES NORMAL(lo hago posterior al if antesesor porque primero lo agrego y lo hago seleccionado)
        if (!combinationManager.CombineMode)
        {
            panelElements.alpha = 1;
        } 

        return true;
    }

    public bool SelectObject(Molecule molecule)
    {
        // verifico si el objeto estaba seleccionado
        if (selectedObjects.IndexOf(molecule.MoleculeIndex) != -1)
        {
            // Esta molecula ya estaba seleccionada. Se quitará la selección
            selectedObjects.Remove(molecule.MoleculeIndex);
            molecule.Deselect();
            return false;
        }

        // si habia uno seleccionado, lo deselecciono
        if (!combinationManager.CombineMode)
        {
            DeselectAll();
        }

        selectedObjects.Add(molecule.MoleculeIndex);
        molecule.Select();
        //no muestro panel de agregar elementos porque una molecula no se cambia 
        panelElements.alpha = 0;
        return true;
    }

    public void RemoveObject(int toRemove)
    {
        selectedObjects.Remove(toRemove);
    }

    public void SwitchCombineMode(bool newMode)
    {
        // si se salio del modo combinar
        if (!newMode)
        {
            DeselectAll();
        }

        //no muestro panel de agregar elementos cuando se activa el switch
        panelElements.alpha = 0;
    }

    public void DeselectAll()
    {
        selectedObjects = new List<int>();

        foreach (Molecule molecule in moleculeManager.Molecules)
        {
            molecule.Deselect();
        }

        foreach (Atom atom in atomManager.AtomsList)
        {
            atom.Deselect();
        }
    }
}
