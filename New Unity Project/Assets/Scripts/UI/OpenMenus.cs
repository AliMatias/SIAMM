﻿using UnityEngine;
using UnityEngine.UI;

//script que abre el panel de tabla periódica
public class OpenMenus : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;

    public void OpenPanelTextIzq()
    {
        //obtengo el animator del panel
        Animator animator = panel.GetComponent<Animator>();
        bool opened = false;
        //si existe
        if (animator != null)
        {
            //obtengo el booleano que dice si esta abierto, y lo cambio
            opened = animator.GetBool("open");
            animator.SetBool("open", !opened);
        }
        //si hay un botón
        if(button != null)
        {
            //obtengo el texto del botón
            Text buttonText = button.GetComponentInChildren<Text>();
            //y lo cambio según corresponda
            if(!opened)
            {          
                buttonText.text = ">";                
            }
            else
            {
                buttonText.text = "<";
            }
        }
    }

    //metodos cuando tienen un boton el panel 
    public void OpenPanelTextDer()
    {
        //obtengo el animator del panel
        Animator animator = panel.GetComponent<Animator>();
        bool opened = false;
        //si existe
        if (animator != null)
        {
            //obtengo el booleano que dice si esta abierto, y lo cambio
            opened = animator.GetBool("open");
            animator.SetBool("open", !opened);
        }
        //si hay un botón
        if (button != null)
        {
            //obtengo el texto del botón
            Text buttonText = button.GetComponentInChildren<Text>();
            //y lo cambio según corresponda
            if (!opened)
            {
                buttonText.text = "<";
            }
            else
            {
                buttonText.text = ">";
            }
        }
    }

    //se utiliza para el panel de informacion adicional
    public void OpenPanel()
    {
        //obtengo el animator del panel
        Animator animator = panel.GetComponent<Animator>();
        bool opened = false;
        //si existe
        if (animator != null)
        {
            //obtengo el booleano que dice si esta abierto, y lo cambio
            opened = animator.GetBool("open");
            animator.SetBool("open", !opened);
        }
    }
}
