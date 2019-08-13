using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    string[] bottonNames = { "Nuevo Proyecto", "Abrir Proyecto","Guardar", "Guardar Como", "Exportar", "Cerrar Proyecto"};

    float posX = -2 * Screen.width;
    float posY = 0.05f * Screen.height;
    float largo = 0.15f * Screen.width;
    float altura = 0.5f * Screen.height;
    //a donde tiene que terminar de posicionarse el menu luego de leslizar
    float posXr = 0.01f * Screen.width;

    float velocidad = 15f;

    bool entrando = true;

    // Update is called once per frame
    void Update()
    {
        if(entrando && posXr != posX)
        {
            posX = Mathf.Lerp(posX, posXr, velocidad * Time.deltaTime);
        }
    }


    void OnGUI()
    {
        posXr = 0.01f * Screen.width;
        posY = 0.05f * Screen.height;//esto puede ir cambiando de acuerdo a donde ubique el boton...
        largo = 0.12f * Screen.width;
        altura = 0.32f * Screen.height;

        GUI.Box(new Rect(posX, posY, largo, altura), "Menú");

        largo = 0.16f * Screen.width;

        GUI.Box(new Rect(posX + 0.005f * Screen.width,
                posY+ (0.03f + 0*0.1f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[0]);

        for (int i=1; i<bottonNames.Length; i++)
        {
            //luego sera un bton interactivo
            GUI.Box(new Rect(posX +0.005f * Screen.width,
                posY+ (0.03f + i*0.05f) * Screen.height,
                largo - 0.05f * Screen.width,
                0.1f * Screen.height - 0.07f * Screen.height), bottonNames[i]);
           
            //-0.07f -> ancho -0.05f el largo
        }

    }
}
