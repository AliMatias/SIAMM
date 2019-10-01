using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    
    #region atributos

    private CombinationManager combinationManager;
    private AtomManager atomManager;
    private MoleculeManager moleculeManager;
    private MaterialManager materialManager;
    private List<int> selectedObjects;
    
    //panel de cambios en particulas subatomicas que tiene que aparecer o no depende del contexto
    private GameObject panelElements;
   
    //panel de info
    private MainInfoPanel mainInfoPanel;

    //hace referencia desde la clase selection manager
    public List<int> SelectedObjects { get => selectedObjects; }

    public GameObject PanelElements { get => panelElements; }

    #endregion

    private void Awake()
    {
        selectedObjects = new List<int>();
        combinationManager = FindObjectOfType<CombinationManager>();
        atomManager = FindObjectOfType<AtomManager>();
        moleculeManager = FindObjectOfType<MoleculeManager>();
        materialManager = FindObjectOfType<MaterialManager>();
        mainInfoPanel = FindObjectOfType<MainInfoPanel>();
        //panel superior del canvas
        panelElements = GameObject.Find("InteractivePanelElements");
    }

    public bool SelectObject(Atom atom)
    {
        // verifico si el objeto estaba seleccionado
        if (selectedObjects.IndexOf(atom.AtomIndex) != -1)
        {
            // Este átomo ya estaba seleccionado. Se quitará la selección
            selectedObjects.Remove(atom.AtomIndex);
            atom.Deselect();

            //si se quita la seleccion y no hay otros seleccionados cierra panel
            mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanel();

            //no muestro panel de agregar elementos
            panelElements.GetComponent<CanvasGroup>().alpha = 0;
            return false;
        }

        // si habia uno seleccionado, lo deselecciono
        if (!combinationManager.CombineMode)
        {
            DeselectAll();
        }

        selectedObjects.Add(atom.AtomIndex);
        atom.Select();

        //seteo info en panel inferior de elementos
        mainInfoPanel.SetInfo(atom);
        //hay que controlar SI no esta abierto otro de los menues!
        mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanelCombine();

        //muestro ademas el panel de agregar elementos! 
        //SI EL MODO ES NORMAL(lo hago posterior al if antesesor porque primero lo agrego y lo hago seleccionado)
        if (!combinationManager.CombineMode)
        {
            panelElements.GetComponent<CanvasGroup>().alpha = 1;
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

            //si se quita la seleccion y no hay otros seleccionados cierra panel
            mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanel();

            return false;
        }

        // si habia uno seleccionado, lo deselecciono
        if (!combinationManager.CombineMode)
        {
            DeselectAll();
        }

        selectedObjects.Add(molecule.MoleculeIndex);
        molecule.Select();

        //seteo info en panel inferior de elementos
        mainInfoPanel.SetInfoMolecule(molecule);
        //hay que controlar SI no esta abierto otro de los menues!
        mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanelCombine();

        //no muestro panel de agregar elementos porque una molecula no se cambia 
        panelElements.GetComponent<CanvasGroup>().alpha = 0;
        return true;
    }

    public bool SelectObject(MaterialObject material)
    {
        // verifico si el objeto estaba seleccionado
        if (selectedObjects.IndexOf(material.MaterialIndex) != -1)
        {
            // Este material ya estaba seleccionada. Se quitará la selección
            selectedObjects.Remove(material.MaterialIndex);
            material.Deselect();

            //si se quita la seleccion y no hay otros seleccionados cierra panel
            mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanel();

            return false;
        }

        // si habia uno seleccionado, lo deselecciono  ->>> CREO QUE ACA ESTA EL PROBLEMA DEL BUG, QUE PIERDE EL INDEX DE LA SELECCION.. AL MOMENTO DE COMBINAR!
        if (!combinationManager.CombineMode)
        {
            DeselectAll();
        }

        selectedObjects.Add(material.MaterialIndex);
        material.Select();

        //seteo info en panel inferior de elementos
        mainInfoPanel.SetInfoMaterial(material);
        //hay que controlar SI no esta abierto otro de los menues!
        mainInfoPanel.GetComponent<OpenMenus>().CloseBottomPanelCombine();

        //no muestro panel de agregar elementos porque no es un atomo
        panelElements.GetComponent<CanvasGroup>().alpha = 0;
        return true;
    }

    public void RemoveObject(int toRemove)
    {
        selectedObjects.Remove(toRemove);
    }

    public void SwitchCombineMode(bool combineMode)
    {
        // si se salio del modo combinar
        if (!combineMode)
        {
            DeselectAll();
        }
        else
        {
            // en el modo combinacion no se pueden seleccionar materiales
            //DeselectAllMaterials();

            //CON ESTO ESTARIA ARREGLANDO EL BUG QUE AL OCMBINAR SALTA EL POPUP DE QUE FALTAN ELEGIR ELEMENTOS.. CHARLARLO LUEGO
            DeselectAll();

            //no muestro panel de agregar elementos cuando se activa el switch
            panelElements.GetComponent<CanvasGroup>().alpha = 0;
        }
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

        foreach (MaterialObject material in materialManager.Materials)
        {
            material.Deselect();
        }
    }

    public void DeselectAllMaterials()
    {
        selectedObjects = new List<int>();

        foreach (MaterialObject material in materialManager.Materials)
        {
            material.Deselect();
        }
    }
}
