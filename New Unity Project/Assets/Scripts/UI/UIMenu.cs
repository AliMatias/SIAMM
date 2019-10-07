using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    string[] bottonNames = { "Nuevo Proyecto", "Abrir Proyecto","Guardar", "Guardar Como", "Exportar", "Salir de SIAMM"};

    float posX = -2 * Screen.width;
    float posY = 0.05f * Screen.height;
    float largo = 0.15f * Screen.width;
    float altura = 0.5f * Screen.height;
    //a donde tiene que terminar de posicionarse el menu luego de Deslizar
    float posXr = 0.01f * Screen.width;

    float velocidad = 15f;

    bool entrando = true;

    //efecto del deslizamiento desde el lateral
    void Update()
    {
        if(entrando && posXr != posX)
        {
            posX = Mathf.Lerp(posX, posXr, velocidad * Time.deltaTime);
        }

        if (!entrando && -2 * Screen.width != posX)
        {
            posX = Mathf.Lerp(posX, -2*Screen.width, velocidad * Time.deltaTime);
        }
    }


    void OnGUI()
    {
        posXr = 0.01f * Screen.width;
        posY = 0.05f * Screen.height;//esto puede ir cambiando de acuerdo a donde ubique el boton...
        largo = 0.12f * Screen.width;
        altura = 0.32f * Screen.height;

        //titulo del menu!
        GUI.Box(new Rect(posXr, posY, largo, altura), "Menú");

        //
        largo = 0.16f * Screen.width;

        //primer boton del menu lo hace aparte para tener una nueva referencia por los anchos
        //GUI.Box(new Rect(posXr + 0.005f * Screen.width,
        //        posY+ (0.03f + 0*0.1f) * Screen.height,
        //        largo - 0.05f * Screen.width,
        //        0.1f * Screen.height - 0.07f * Screen.height), bottonNames[0]);


        //primer boton del menu lo hace aparte para tener una nueva referencia por los anchos
        if (GUI.Button(new Rect(posXr + 0.005f * Screen.width,
                posY + (0.03f + 0 * 0.1f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[0]))
        {
            showMsge(bottonNames[0]);
        }



        //demas botones! 
        for (int i=1; i<bottonNames.Length; i++)
        {
            //luego sera un bton interactivo
            //GUI.Box(new Rect(posXr + 0.005f * Screen.width,
            //    posY+ (0.03f + i*0.05f) * Screen.height,
            //    largo - 0.05f * Screen.width,
            //    0.1f * Screen.height - 0.07f * Screen.height), bottonNames[i]);

            //-0.07f -> ancho -0.05f el largo


            if (GUI.Button(new Rect(posXr + 0.005f * Screen.width,
                posY + (0.03f + i * 0.05f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[i]))
            {
                showMsge(bottonNames[i]);
            }
        }


    }


    private void showMsge (string msge)
    {
        Debug.Log("APRETASTE GIL! " + msge);
    }
}
