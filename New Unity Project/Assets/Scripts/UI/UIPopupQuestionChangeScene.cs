using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupQuestionChangeScene : MonoBehaviour
{

    private bool showPopUp = false;
    private string popUpMessage = "";
    private string popUpTitle = "";
    private int sceneIndex;
    private Image backgroundLayer;

    private SceneManager sceneManager;

    public bool ShowPopUp { get => showPopUp; set => showPopUp = value; }
    public string PopUpMessage { get => popUpMessage; set => popUpMessage = value; }
    public string PopUpTitle { get => popUpTitle; set => popUpTitle = value; }

    public void MostrarPopUp(string title, string message, int sceneIndex, Image backgroundLayer)
    {
        popUpTitle = title;
        popUpMessage = message;
        this.showPopUp = true;
        this.sceneIndex = sceneIndex;
        this.backgroundLayer = backgroundLayer;
    }

    public void OcultarPopUp()
    {
        showPopUp = false;
    }

    public void OnGUI()
    {
        if (showPopUp)
        {
            backgroundLayer.enabled = true;
            // Arma el pop up
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 150), ShowGUI, popUpTitle);
        }
    }

    //INSTANCIA UNA VEZ para la interfaz que lo referencia por eso hay MAS DE UNA clase uipopup
    public void ShowGUI(int windowID)
    {
        GUI.color = new Color(1, 1, 1, 1.0f);
        // Label que muestra mensaje
        GUI.Label(new Rect(65, 40, 200, 50), popUpMessage);

        // Boton OK para cerrar
        if (GUI.Button(new Rect(50, 100, 75, 30), "Sí"))
        {
            OcultarPopUp();
            backgroundLayer.enabled = false;
            SceneManager.LoadSceneAsync(sceneIndex);
        }
        else if (GUI.Button(new Rect(200, 100, 75, 30), "No"))
        {
            OcultarPopUp();
            backgroundLayer.enabled = false;
        }
    }
}
