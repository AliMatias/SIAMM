using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelClick : MonoBehaviour
{
    private bool entrando = false;

    void Update()
    {
        if (entrando)
        {
           gameObject.GetComponent<Lean.Gui.LeanDrag>().clickMenuOpenClose(entrando);//usa metodos especiales dentro del contexto del script original
        }

        else
        {
            gameObject.GetComponent<Lean.Gui.LeanDrag>().clickMenuOpenClose(entrando);//usa metodos especiales dentro del contexto del script original
        }
    }

    //metodo para UIAction
    public void CloseMenu()
    {
        entrando = false;
        Destroy(this, 0.5f);
    }

    //metodo para UIAction
    public void OpenMenu()
    {
        entrando = true;
    }
}
