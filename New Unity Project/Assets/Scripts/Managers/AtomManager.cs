using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AtomManager : MonoBehaviour
{
    #region Atributos 
        //prefab asignado por interfaz
        [SerializeField]
        private Atom atomPrefab;
        //lista que maneja los átomos en pantalla
        private List<Atom> atomsList = new List<Atom>();
        //booleano que indica si el modo combinación está activo
        private bool combineMode = false;
        private PositionManager positionManager = PositionManager.Instance;
        private SelectionManager selectionManager;
        private SuggestionManager suggestionManager;
        private TipsManager tipsManager;

        //lista de botones relevantes para los átomos
        private List<Button> atomButtons = new List<Button>();
        [SerializeField]
        private Button plusAtomButton;

        private UIPopup popup;

        public List<Atom> AtomsList { get => atomsList; }

        private CombinationManager combinationManager;
        private UIToolTipControl openTper;
    #endregion

    private void Awake()
    {
        //GameObject[] buttons = GameObject.FindGameObjectsWithTag("atomToggle");
        //foreach(GameObject btn in buttons)
        //{
        //    atomButtons.Add(btn.GetComponent<Button>());
        //}
        activateDeactivateAtomButtons();

        popup = FindObjectOfType<UIPopup>();
        selectionManager = FindObjectOfType<SelectionManager>();
        suggestionManager = FindObjectOfType<SuggestionManager>();
        tipsManager = FindObjectOfType<TipsManager>();

        combinationManager = FindObjectOfType<CombinationManager>();
        openTper = FindObjectOfType<UIToolTipControl>();
    }

    //agregar nuevo átomo al espacio de trabajo
    public void NewAtom(bool withProton)
    {
        //intento obtener una posición disponible 
        int position;
        try
        {
            position = positionManager.GetFirstAvailablePositionIndex();
        }
        //si no hay mas posiciones disponibles, lo loggeo y me voy
        catch(NoPositionsLeftException nple)
        {
            //no va popup -> consultar si no se deberia mostrar error
            Debug.Log(nple.Message);
            return;
        }
        //instancio
        Atom spawnedAtom = Instantiate<Atom>(atomPrefab);
        //asigno random position
        spawnedAtom.transform.localPosition = positionManager.Positions[position];
        //agrego a la lista
        atomsList.Add(spawnedAtom);
        //asigno su índice a este átomo
        spawnedAtom.AtomIndex = position;
        //si dice que lo spawnee con un proton, lo hago
        if(withProton){
            spawnedAtom.SpawnNucleon(true, false);
        }
        SelectAtom(spawnedAtom.AtomIndex);

        suggestionManager.updateSuggestions();
        activateDeactivateAtomButtons();

        /*CREA UN TIP! CON LA TEMATICA PASADA POR ID*/
        tipsManager.LaunchTips(6);
    }

    public void NewAtom(AtomSaveData atomSaveData){
        int position = atomSaveData.position;
        //instancio
        Atom spawnedAtom = Instantiate<Atom>(atomPrefab);
        //asigno position
        spawnedAtom.transform.localPosition = positionManager.Positions[position];
        //agrego a la lista
        atomsList.Add(spawnedAtom);
        //asigno su índice a este átomo
        spawnedAtom.AtomIndex = position;
        spawnedAtom.SpawnFromSaveData(atomSaveData.protons, atomSaveData.neutrons, atomSaveData.electrons);
        suggestionManager.updateSuggestions();
        activateDeactivateAtomButtons();
    }

    //seleccionar átomo
    public void SelectAtom(int index)
    {
        Atom selectedAtom = FindAtomInList(index);
        selectionManager.SelectObject(selectedAtom);
        suggestionManager.updateSuggestions();
    }

    //ya que el índice del átomo depende de la posición, 
    //necesito recorrer la lista para encontrarlo
    public Atom FindAtomInList(int index){
        foreach(Atom atom in atomsList){
            if(atom.AtomIndex == index){
                return atom;
            }
        }
        return null;
    }

    //comienza el brillo en el átomo seleccionado
    private void SelectParticlesFromAtom(int index)
    {
        Atom atom = FindAtomInList(index);
        atom.Select();
    }

    //agregar la partícula indicada al átomo seleccionado
    public void AddParticleToSelectedAtom(int particle){
        List<int> selectedObjects = selectionManager.SelectedObjects;
        Atom atom = selectedObjects.Count > 0 ?
            FindAtomInList(selectedObjects.Last()) :
            null;

        if (atom == null) {
            Debug.Log("No hay ningún átomo seleccionado");
            popup.MostrarPopUp("Manager Átomo", "No hay ningún átomo seleccionado");   
            return;
        }
        //agarro el átomo indicado de la lista
        if(particle==0){
            atom.SpawnNucleon(true, false);
        }else if (particle==1){
            atom.SpawnNucleon(false, false);
        }else if(particle ==2){ //agregara electrones
            atom.SpawnElectron(false);
        }else{
            //no va popup
            Debug.Log("Se ingreso un índice de partícula equivocado.");
            Debug.Log("Los valores correctos son: 0-protón, 1-neutrón, 2-electrón");
            return;
        }
        suggestionManager.updateSuggestions();
    }

    //quitar del átomo seleccionado la partícula indicada
    public void RemoveParticleFromSelectedAtom(int particle){
        List<int> selectedObjects = selectionManager.SelectedObjects;
        Atom atom = selectedObjects.Count > 0 ?
            FindAtomInList(selectedObjects.Last()) :
            null;

        if (atom == null)
        {
            Debug.Log("No hay ningún átomo seleccionado");
            popup.MostrarPopUp("Manager Átomo", "No hay ningún átomo seleccionado");
   
            return;
        }
        //agarro el átomo de la lista
        if(particle==0){
            atom.RemoveProton();
        }else if (particle==1){
            atom.RemoveNeutron();
        }else if(particle ==2){
            atom.RemoveElectron();
        }else{
            //no va popup
            Debug.Log("Se ingreso un índice de partícula equivocado.");
            Debug.Log("Los valores correctos son: 0-protón, 1-neutrón, 2-electrón");
            return;
        }
        suggestionManager.updateSuggestions();
    }

    //BORRAR átomo seleccionado.
    public void DeleteSelectedAtom(){
        List<int> selectedObjects = selectionManager.SelectedObjects;
        Atom atom = selectedObjects.Count > 0 ?
            FindAtomInList(selectedObjects.Last()) :
            null;

        if (atom == null)
        { 
            Debug.Log("No hay ningún átomo seleccionado");
            popup.MostrarPopUp("Manager Átomo", "No hay ningún átomo seleccionado");
    
            return;
        }
        DeleteAtom(atom.AtomIndex);
    }

    //BORRAR átomo
    public void DeleteAtom(int index)
    {
        Atom atom = FindAtomInList(index);
        //lo saco de la lista
        atomsList.Remove(atom);
        selectionManager.RemoveObject(index);
        //lo destruyo
        Destroy(atom);
        //disponibilizo la posición denuevo
        positionManager.AvailablePositions[index] = true;

        //no va popup
        Debug.Log("Se ha borrado el átomo " + index);

        suggestionManager.updateSuggestions();
        activateDeactivateAtomButtons();
    }

    //spawnear átomo seleccionado en la tabla periódica
    public void SpawnFromPeriodicTable(string elementName)
    {
        int oldAtomsCount = atomsList.Count;
        NewAtom(false);
        int newAtomsCount = atomsList.Count;

        if (oldAtomsCount < newAtomsCount)
        {
            Atom newAtom = atomsList[newAtomsCount-1];
            try
            {
                newAtom.SpawnFromPeriodicTable(elementName);
                suggestionManager.updateSuggestions();
            } 
            catch(SpawnException)
            {
                //hubo un error y no tiene que GUARDAR la posicion la tiene que liberar como ELIMINAR ATOMO (a completar)
                Debug.Log("Se libera Posicion tomada, porque dio error al intentar spawn");
            }
        }
        else
        {
            Debug.Log("No hay más lugar para crear un nuevo átomo.");
            popup.MostrarPopUp("Manager Átomo", "No hay más lugar para crear un nuevo átomo");
        }
        activateDeactivateAtomButtons();
    }

    public void SpawnFromSaveData(AtomSaveData atomSaveData){
        if(positionManager.OccupyPosition(atomSaveData.position)){
            NewAtom(atomSaveData);
        }else{
            Debug.LogError("Átomo en posición " + atomSaveData.position + " no cargado");
        }
    }

    //activa-desactiva botones de acuerdo a la cant de átomos
    private void activateDeactivateAtomButtons()
    {
        //bool status = true;
        //if(atomsList.Count == 0){
        //    status = false;
        //}

        //foreach(Button btn in atomButtons){
        //    btn.interactable = status;
        //}

        if(positionManager.NoPositionsLeft())
        {
            plusAtomButton.interactable = false;
        }
        else
        {
            //aca deberia controlar por las dudas que no este en modo combinacion.. para que no active el boton..
            if (combinationManager != null && combinationManager.CombineMode == false) 
            {
                if (openTper.panelTper.activeSelf == false)//si la tabla periodica esta abierta! no habilita!
                    plusAtomButton.interactable = true;
            }
        }
    }

    public List<int> GetSelectedAtoms()
    {
        List<int> selectedAtoms = new List<int>();
        List<int> selectedObjects = selectionManager.SelectedObjects;
        foreach (int index in selectedObjects)
        {
            Atom atom = FindAtomInList(index);
            if (atom != null)
            {
                selectedAtoms.Add(index);
            }
        }
        return selectedAtoms;
    }

    public List<int> GetSelectedAtomNumbers()
    {
        List<int> selectedAtoms = new List<int>();
        List<int> selectedObjects = selectionManager.SelectedObjects;
        foreach (int index in selectedObjects)
        {
            Atom atom = FindAtomInList(index);
            if (atom != null)
            {
                selectedAtoms.Add(atom.ElementNumber);
            }
        }
        return selectedAtoms;
    }

    //retorna el tipo de ATOM seleccionado (metodo especial para Panel Inferior Info), ya se de antemano que hay 1 solo seleccionado
    public TypeAtomEnum GetTypeSelectedAtoms()
    {
        List<int> selectedObjects = selectionManager.SelectedObjects;
        Atom atom = FindAtomInList(selectedObjects.Max());//uso max, como es 1 solo item.. me da igual solo quiero obtener lo que hay.. 
        return atom.TypeAtom;            
    }


    /**
     * Elimina todos los atomos
     */
    public void DeleteAllAtoms()
    {
        List<Atom> toDelete = new List<Atom>(atomsList);
        foreach (Atom atom in toDelete)
        {
            DeleteAtom(atom.AtomIndex);
        }
        //el panel de las subparticulas se tiene que ocultar
        selectionManager.PanelElements.GetComponent<CanvasGroup>().alpha = 0;
    }
}
