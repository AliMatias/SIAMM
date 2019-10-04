using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    private const int LINE_HEIGHT = 20;
    private bool showPopUp = false;
    private string popUpMessage = "";
    private string popUpTitle = "";
    private int extraLines = 0;

    public bool ShowPopUp { get => showPopUp; set => showPopUp = value; }
    public string PopUpMessage { get => popUpMessage; set => popUpMessage = value; }
    public string PopUpTitle { get => popUpTitle; set => popUpTitle = value; }

    public void MostrarPopUp(string title, string message)
    {
        PopUpTitle = title;
        PopUpMessage = message;
        showPopUp = true;
        extraLines = 0;
    }

    public void MostrarPopUp(string title, string message, int extraLines)
    {
        PopUpTitle = title;
        PopUpMessage = message;
        showPopUp = true;
        this.extraLines = extraLines;
    }

    public void OcultarPopUp()
    {
        showPopUp = false;
    }

    public void OnGUI()
    {
        if (showPopUp)
        {
            // Arma el pop up
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 150 + (extraLines * LINE_HEIGHT)), ShowGUI, popUpTitle);
        }
    }

    //INSTANCIA UNA VEZ para la interfaz que lo referencia por eso hay 2 clases uipopup
    public void ShowGUI(int windowID)
    {
        // Label que muestra mensaje
        GUI.Label(new Rect(65, 40, 200, 50 + (extraLines * LINE_HEIGHT)), popUpMessage);

        // Boton OK para cerrar
        if (GUI.Button(new Rect(50, 100 + (extraLines * LINE_HEIGHT), 75, 30), "OK"))
        {
            OcultarPopUp();
        }

    }
}
