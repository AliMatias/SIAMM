using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Esta clase se va a encargar de desactivar y activar los diferentes elementos de la UI cuando corresponda
public class UIManager : MonoBehaviour
{
    List<Button> buttonsToToggle = new List<Button>();

    void Awake()
    {
        GameObject[] btns = GameObject.FindGameObjectsWithTag("toToggle");
        foreach(GameObject btn in btns){
            buttonsToToggle.Add(btn.GetComponent<Button>());
        }
    }

    public void SwitchCombineMode(bool combineMode){
        foreach(Button btn in buttonsToToggle){
            btn.interactable = !combineMode;
        }
    }
}
