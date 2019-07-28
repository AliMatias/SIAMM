using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private CombinationManager combinationManager;
    private AtomManager atomManager;
    private List<int> selectedObjects;

    public List<int> SelectedObjects { get => selectedObjects; }


    private void Awake()
    {
        selectedObjects = new List<int>();
        combinationManager = FindObjectOfType<CombinationManager>();
        atomManager = FindObjectOfType<AtomManager>();
    }

    public bool SelectObject(Atom atom, List<Atom> atomList)
    { 
        // verifico si el objeto estaba seleccionado
        if (selectedObjects.IndexOf(atom.AtomIndex) != -1)
        {
            // Este átomo ya estaba seleccionado. Se quitará la selección
            selectedObjects.Remove(atom.AtomIndex);
            atom.Deselect();
            return false;
        }

        // si habia uno seleccionado, lo deselecciono
        if (!combinationManager.CombineMode)
        {
            selectedObjects = new List<int>();
            foreach (Atom atomInList in atomList)
            {
                atomInList.Deselect();
            }
        }

        selectedObjects.Add(atom.AtomIndex);
        atom.Select();
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
            atomManager.DeselectAll();
        }
    }
}
