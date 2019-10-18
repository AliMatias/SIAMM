using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTip : MonoBehaviour
{
    private TipsManager tipsManager;
    private bool onePass = false;

    void Start()
    {
        tipsManager = FindObjectOfType<TipsManager>();
    }

    /*Metodo para boton de tabla Periodica*/
    public void activateTipTper(int idTip)
    {
        //solo se ejecuta si la t per se esta por mostrar! fix si se activa desde el boton del panel
        if (!onePass)
        {
            tipsManager.LaunchTips(idTip);
            onePass = true;
        }
        else
        {
            onePass = false;
        }
    }
}
