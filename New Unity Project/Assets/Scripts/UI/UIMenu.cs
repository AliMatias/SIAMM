using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    private string[] bottonNames = { "Nuevo Proyecto", "Abrir Proyecto", "Carga Rápida", "Guardado Rápido", "Guardar Como", "Desactivar TIPS" ,"Salir de SIAMM"};

    private float posX = -2 * Screen.width; //posicion inicial (detras del canvas al costado)                                          
    private float posXr = 0.03f * Screen.width; //a donde tiene que terminar de posicionarse el menu luego de Deslizar
    private float posY = 0.07f * Screen.height;
    private float largo = 0.12f * Screen.width;
    private float altura = 0.37f * Screen.height;

    private float velocidad = 15f;
    private bool entrando = true;
    private GUIStyle currentStyle = null;

    //funcionalidades de guardado espacio de trabajo
    private SaveLoadManager sl;

    public bool Entrando { get => entrando; set => entrando = value; }

    void Awake()
    {
        sl = FindObjectOfType<SaveLoadManager>();
    }

    //efecto del deslizamiento desde el lateral
    void Update()
    {
        //apertura
        if (entrando && posXr != posX)
        {
            posX = Mathf.Lerp(posX, posXr, velocidad * Time.deltaTime);
        }

        //para el cierre del menu
        if (!entrando && -2 * Screen.width != posX)
        {
            posX = Mathf.Lerp(posX, -2 * Screen.width, 0.3f * Time.deltaTime);
        }
    }

    //creacion del menu
    void OnGUI()
    {
        InitStyles();

        //valores del "background donde van los botones"
        posXr = 0.03f * Screen.width;//a donde tiene que terminar de posicionarse el menu luego de Deslizar     
        posY = 0.07f * Screen.height;//esto puede ir cambiando de acuerdo a donde ubique el boton... desde arriba
        largo = 0.12f * Screen.width;
        altura = 0.37f * Screen.height;

        //titulo del menu!
        GUI.Box(new Rect(posX, posY, largo, altura), "Menú", currentStyle);

        //nueva posicion para los botones asi bajan dejando lugar al titulo
        largo = 0.16f * Screen.width;

        //primer boton del menu lo hace aparte para tener una nueva referencia por los anchos
        if (GUI.Button(new Rect(posX + 0.005f * Screen.width,
                posY + (0.035f + 0 * 0.1f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[0]))
        {
            CallFuntion(bottonNames[0]);
        }

        checkOptionTips();

        //demas botones tomando como referencia el 1ro! 
        for (int i=1; i<bottonNames.Length; i++)
        {
            if (GUI.Button(new Rect(posX + 0.005f * Screen.width,
                posY + (0.03f + i * 0.05f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[i]))
            {
                CallFuntion(bottonNames[i]);
            }
        }
    }

    //utilizo texturas para controlar la opacidad
    private void InitStyles()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(0, 0, 0, 0.95f));//controlar la opacidad rgb es BLACK!
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

    //metodo para que sea llamado desde el UIaction y haga el fade a la izq
    public void CloseMenu()
    {
        Entrando = false;
        Destroy(this, 0.5f);//en in cierto tiempo delta se cierra
    }

    private void CallFuntion (string msge)
    {
        //agrego el script de acciones a menu en runtime
        gameObject.AddComponent<UIAction>();

        switch (msge)
        {
            case "Nuevo Proyecto":              
                GetComponent<UIAction>().newProy();//tiene que llamar un popup de afirmacion
                break;

            case "Abrir Proyecto":
                sl.OpenFile();
                break;

            case "Guardado Rápido":
                sl.Save();
                break;

            case "Guardar Como":
                sl.SaveAs();
                break;

            case "Carga Rápida":
                sl.Load(); 
                break;

            case "Desactivar TIPS":
                GetComponent<UIAction>().OptionsTips();
                break;

            case "Activar TIPS":
                GetComponent<UIAction>().OptionsTips();
                break;

            case "Salir de SIAMM":
                GetComponent<UIAction>().Quit();//tiene que llamar un popup de afirmacion
                break;
        }
    }

    /*control para colocar el nombre al boton segun este activo o no los tips*/
    private void checkOptionTips()
    {
        TipsManager tipsManager;
        //no ejecuta el awake por eso se coloca aca las referencias
        tipsManager = FindObjectOfType<TipsManager>();

        if (tipsManager.DisableTips)
        {
            bottonNames[5] = "Activar TIPS";
        }
        else
        {
            bottonNames[5] = "Desactivar TIPS";
        }
    }
}
