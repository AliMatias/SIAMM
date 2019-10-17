using UnityEngine;

public class UIPopupQuestionTips : MonoBehaviour
{

    private bool showPopUp = false;
    private string popUpMessage = "";
    private string popUpTitle = "";
    private GUIStyle currentStyle = null;

    private TipsObject tips;
    private TipsManager tipsManager;


    public bool ShowPopUp { get => showPopUp; set => showPopUp = value; }
    public string PopUpMessage { get => popUpMessage; set => popUpMessage = value; }
    public string PopUpTitle { get => popUpTitle; set => popUpTitle = value; }

    public void MostrarPopUp(string title, string message)
    {
        PopUpTitle = title;
        PopUpMessage = message;
        showPopUp = true;
    }

    public void OcultarPopUp()
    {
        showPopUp = false;
    }

    public void OnGUI()
    {
        InitStyles();
        if (showPopUp)
        {
            // Arma el pop up
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 200, 170), ShowGUI, popUpTitle, currentStyle);
        }
    }

    //INSTANCIA UNA VEZ para la interfaz que lo referencia por eso hay MAS DE UNA clase uipopup
    public void ShowGUI(int windowID)
    {
        //no ejecuta el awake por eso se coloca aca las referencias
        tips = FindObjectOfType<TipsObject>();
        tipsManager = FindObjectOfType<TipsManager>();

        // Label que muestra mensaje
        GUI.Label(new Rect(30, 30, 200, 50), popUpMessage);

        // Boton OK para cerrar
        if (GUI.Button(new Rect(50, 70, 100, 20), "Ocultar Tip"))
        {
            OcultarPopUp();
            tipsManager.DeleteTip(tips);
        }

        else if (GUI.Button(new Rect(20, 100, 160, 20), "Deshabilitar este Tip"))
        {
            OcultarPopUp();
            tipsManager.setDisabledTipId(tips.IdTip);
            tipsManager.DeleteTip(tips);
        }
        else if (GUI.Button(new Rect(10, 130, 180, 20), "Deshabilitar TODOS los Tips"))
        {
            OcultarPopUp();
            tipsManager.setDisabledAllTips();
            tipsManager.DeleteTip(tips);
        }
    }

    //utilizo texturas para controlar la opacidad
    private void InitStyles()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(0, 0, 0, 0.35f));//controlar la opacidad rgb es BLACK!
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
