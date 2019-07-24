using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    private UIPopupQuestionQuit popupQuit;

    public void Quit()
    {
        popupQuit = FindObjectOfType<UIPopupQuestionQuit>();

        Debug.Log("Saliendo de la Aplicación");

        popupQuit.MostrarPopUp("Salir", "¿Esta seguro que desea salir de la aplicacion?");
    }

}
