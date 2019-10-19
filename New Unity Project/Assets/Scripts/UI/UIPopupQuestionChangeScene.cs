using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupQuestionChangeScene : MonoBehaviour
{

    private bool showPopUp = false;
    private bool showErrorPopUp = false;
    private string popUpMessage = "";
    private string popUpTitle = "";
    private int sceneIndex;
    [SerializeField]
    public Image backgroundLayer;

    private SceneManager sceneManager;

    public bool ShowPopUp { get => showPopUp; set => showPopUp = value; }
    public bool ShowErrorPopUp { get => showErrorPopUp; set => showErrorPopUp = value; }
    public string PopUpMessage { get => popUpMessage; set => popUpMessage = value; }
    public string PopUpTitle { get => popUpTitle; set => popUpTitle = value; }

    public void MostrarPopUp(string title, string message, int sceneIndex)
    {
        popUpTitle = title;
        popUpMessage = message;
        this.showPopUp = true;
        this.sceneIndex = sceneIndex;
    }

    public void MostrarErrorPopUp(string title, string message, int sceneIndex)
    {
        popUpTitle = title;
        popUpMessage = message;
        this.showErrorPopUp = true;
        this.sceneIndex = sceneIndex;
    }

    public void OcultarPopUp()
    {
        showPopUp = false;
        showErrorPopUp = false;
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
        else if (showErrorPopUp)
        {
            backgroundLayer.enabled = true;
            // Arma el pop up
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 150), ShowErrorGUI, popUpTitle);
        }
    }

    public void ShowGUI(int windowID)
    {
        GUI.color = new Color(1, 1, 1, 1.0f);
        // Label que muestra mensaje
        GUI.Label(new Rect(65, 40, 200, 50), popUpMessage);

        if (GUI.Button(new Rect(50, 100, 75, 30), "Sí"))
        {
            OcultarPopUp();
            SceneManager.LoadSceneAsync(sceneIndex);
        }
        else if (GUI.Button(new Rect(200, 100, 75, 30), "No"))
        {
            backgroundLayer.enabled = false;
            OcultarPopUp();
        }
    }

    public void ShowErrorGUI(int windowID)
    {
        GUI.color = new Color(1, 1, 1, 1.0f);
        // Label que muestra mensaje
        GUI.Label(new Rect(65, 40, 200, 50), popUpMessage);

        if (GUI.Button(new Rect(50, 100, 75, 30), "OK"))
        {
            OcultarPopUp();
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
