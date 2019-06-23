using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Esta clase se va a encargar de desactivar y activar los diferentes elementos de la UI cuando corresponda
public class CombinationManager : MonoBehaviour
{
    private List<Button> buttonsToToggle = new List<Button>();
    private bool combineMode = false;
    private AtomManager atomManager;
    public Button combineButton;
    public Button combineModeButton;

    void Awake()
    {
        atomManager = FindObjectOfType<AtomManager>();
        GameObject[] btns = GameObject.FindGameObjectsWithTag("toToggle");
        foreach(GameObject btn in btns){
            buttonsToToggle.Add(btn.GetComponent<Button>());
        }
        combineButton.interactable = false;
    }

    public void SwitchCombineMode(){
        combineMode = !combineMode;
        foreach(Button btn in buttonsToToggle){
            btn.interactable = !combineMode;
        }
        combineButton.interactable = !combineButton.interactable;
        atomManager.SwitchCombineMode();
        Text text = combineModeButton.GetComponentInChildren<Text>();
        if(combineMode){
            text.text = "Modo normal";
        }else {
            text.text = "Modo combinación";
        }
    }

    //Acá tiene que ir a la bd a buscar la combinación
    public void CombineAtoms(){
        List<int> selectedAtoms = atomManager.SelectedAtoms;
        string combination = "Estás combinando los átomos: ";
        foreach(int index in selectedAtoms){
            combination += index + " - ";
        }
        Debug.Log(combination);
    }
}
