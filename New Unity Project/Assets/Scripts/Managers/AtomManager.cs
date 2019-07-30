using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AtomManager : MonoBehaviour
{
    //prefab asignado por interfaz
    [SerializeField]
    private Atom atomPrefab;
    //lista que maneja los átomos en pantalla
    private List<Atom> atomsList = new List<Atom>();
    //booleano que indica si el modo combinación está activo
    private bool combineMode = false;
    private PositionManager positionManager = PositionManager.Instance;
    private SelectionManager selectionManager;
    //lista de botones relevantes para los átomos
    private List<Button> atomButtons = new List<Button>();
    [SerializeField]
    private Button plusAtomButton;

    private UIPopup popup;

    public List<Atom> AtomsList { get => atomsList; }

    private void Awake(){
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("toToggle");
        foreach(GameObject btn in buttons)
        {
            atomButtons.Add(btn.GetComponent<Button>());
        }
        activateDeactivateAtomButtons();

        popup = FindObjectOfType<UIPopup>();
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    //agregar nuevo átomo al espacio de trabajo
    public void NewAtom(bool withProton)
    {
        //intento obtener una posición disponible random
        int position;
        try
        {
            position = positionManager.ObtainRandomPositionIndex();
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
        spawnedAtom.transform.localPosition = positionManager.PlanePositions[position];
        //agrego a la lista
        atomsList.Add(spawnedAtom);
        //asigno su índice a este átomo
        spawnedAtom.AtomIndex = position;
        //si dice que lo spawnee con un proton, lo hago
        if(withProton){
            spawnedAtom.SpawnNucleon(true, false);
        }
        SelectAtom(spawnedAtom.AtomIndex);
        activateDeactivateAtomButtons();
    }

    //seleccionar átomo
    public void SelectAtom(int index)
    {
        Atom selectedAtom = FindAtomInList(index);
        selectionManager.SelectObject(selectedAtom);
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

    //activa-desactiva botones de acuerdo a la cant de átomos
    private void activateDeactivateAtomButtons()
    {
        bool status = true;
        if(atomsList.Count == 0){
            status = false;
        }

        foreach(Button btn in atomButtons){
            btn.interactable = status;
        }

        if(positionManager.NoPositionsLeft()){
            plusAtomButton.interactable = false;
        }else{
            plusAtomButton.interactable = true;
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
}
