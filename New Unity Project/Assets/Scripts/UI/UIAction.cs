using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    private UIPopupQuestionQuit popupQuit;
    private bool menu = false;

    public void Quit()
    {
        Debug.Log("Saliendo de la Aplicación");
        popupQuit = FindObjectOfType<UIPopupQuestionQuit>();       
        popupQuit.MostrarPopUp("Salir", "¿Esta seguro que desea salir de la aplicacion?");
    }

    public void newProy()
    {
        Debug.Log("Nuevo Proyecto?");
        gameObject.AddComponent<UIPopupQuestionNewProy>();
        GetComponent<UIPopupQuestionNewProy>().MostrarPopUp("Nuevo Proyecto", "¿Esta seguro que desea borrar todo y comenzar de nuevo?");//tiene que llamar un popup de afirmacion
    }

    public void openMenu()
    {
        if (!menu)
        {
            gameObject.AddComponent<UIMenu>();
            menu = true;
        }
        else if (menu)
        {
            GetComponent<UIMenu>().CloseMenu();
            menu = false;
        }
    }
}
