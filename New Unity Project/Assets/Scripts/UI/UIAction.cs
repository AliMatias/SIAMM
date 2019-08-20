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
        popupQuit = FindObjectOfType<UIPopupQuestionQuit>();

        Debug.Log("Saliendo de la Aplicación");

        popupQuit.MostrarPopUp("Salir", "¿Esta seguro que desea salir de la aplicacion?");
    }


    public void openMenu()
    {
        if (!menu)
        {
            Debug.Log("ABRE MENU");
            gameObject.AddComponent<UIMenu>();
            menu = true;
        }
        else if (menu)
        {
            Destroy(GetComponent<UIMenu>());
            menu = false;
        }
    }



}
