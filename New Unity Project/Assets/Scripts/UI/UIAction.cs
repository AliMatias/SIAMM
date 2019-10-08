using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    private UIPopupQuestionQuit popupQuit;
    private UIPopupQuestionNewProy popupNewProy;
    private bool menu = false;

    public void Quit()
    {
        popupQuit = FindObjectOfType<UIPopupQuestionQuit>();

        Debug.Log("Saliendo de la Aplicación");

        popupQuit.MostrarPopUp("Salir", "¿Esta seguro que desea salir de la aplicacion?");
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


    public void newProy()
    {
        Debug.Log("Nuevo Proyecto?");

        gameObject.AddComponent<UIPopupQuestionNewProy>();
        GetComponent<UIPopupQuestionNewProy>().MostrarPopUp("Nuevo Proyecto", "¿estás seguro que querés borrar todo y comenzar de nuevo?");//tiene que llamar un popup de afirmacion
    }

}
