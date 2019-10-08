using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    private string[] bottonNames = { "Nuevo Proyecto", "Abrir Proyecto","Guardar", "Guardar Como", "Exportar", "Salir de SIAMM"};

    private float posX = -2 * Screen.width; //posicion inicial (detras del canvas al costado)                                          
    private float posXr = 0.03f * Screen.width; //a donde tiene que terminar de posicionarse el menu luego de Deslizar
    private float posY = 0.07f * Screen.height;
    private float largo = 0.12f * Screen.width;
    private float altura = 0.32f * Screen.height;

    private float velocidad = 15f;

    private bool entrando = true;

    //funcionalidades
    private SaveLoadManager sl;

    private GUIStyle currentStyle = null;

    void Awake()
    {
        sl = FindObjectOfType<SaveLoadManager>();
    }

    //efecto del deslizamiento desde el lateral
    void Update()
    {
        if(entrando && posXr != posX)
        {
            posX = Mathf.Lerp(posX, posXr, velocidad * Time.deltaTime);
        }

        if (!entrando && -2 * Screen.width != posX)
        {
            posX = Mathf.Lerp(posX, -2 * Screen.width, 0.3f * Time.deltaTime);
        }
    }


    void OnGUI()
    {

        InitStyles();

        //valores del "background donde van los botones"
        posXr = 0.03f * Screen.width;      
        posY = 0.07f * Screen.height;//esto puede ir cambiando de acuerdo a donde ubique el boton... desde arriba
        largo = 0.12f * Screen.width;
        altura = 0.32f * Screen.height;

        //titulo del menu!
        GUI.Box(new Rect(posX, posY, largo, altura), "Menú",currentStyle);

        //nueva posicion para los botones asi bajan dejando lugar al titulo
        largo = 0.16f * Screen.width;

        //primer boton del menu lo hace aparte para tener una nueva referencia por los anchos
        if (GUI.Button(new Rect(posX + 0.005f * Screen.width,
                posY + (0.03f + 0 * 0.1f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[0]))
        {
            showMsge(bottonNames[0]);
        }

    
        //demas botones tomando como referencia el 1ro! 
        for (int i=1; i<bottonNames.Length; i++)
        {
            if (GUI.Button(new Rect(posX + 0.005f * Screen.width,
                posY + (0.03f + i * 0.05f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[i]))
            {
                showMsge(bottonNames[i]);
            }
        }
    }

    private void InitStyles()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(0, 0, 0, 0.5f));
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

    public void CloseMenu()
    {
        entrando = false;
        Destroy(this, 0.5f);
    }

    private void showMsge (string msge)
    {      
        switch (msge)
        {
            case "Nuevo Proyecto":
                Debug.Log("APRETASTE GIL! " + msge);
                break;

            case "Abrir Proyecto":
                sl.Load();
                break;

            case "Guardar":
                sl.Save();
                break;

            case "Guardar Como":
                Debug.Log("APRETASTE GIL! " + msge);
                break;

            case "Exportar":
                Debug.Log("APRETASTE GIL! " + msge);
                break;

            case "Salir de SIAMM":
                gameObject.AddComponent<UIAction>();
                GetComponent<UIAction>().Quit();
                break;
        }
    }
}
