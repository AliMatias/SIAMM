using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToolTipControl : MonoBehaviour
{
    public GameObject panelTper;
    public CanvasGroup toolTipButton;
    public CanvasGroup toolTipCloseTper;
    public static bool flagTooltip;

    // Start is called before the first frame update
    void Start()
    {
        flagTooltip = false;
    }

    // Update is called once per frame
    void Update()
    {
        //si el panel de la tabla periodica esta inactivo.. siempre tiene que estar el toolipTEXT oculto.. soluciona BUG
        //doble control
        if (!panelTper.activeSelf)
        {
            toolTipButton.alpha = 0;
            toolTipCloseTper.alpha = 0;
        }
   
        if (!flagTooltip)
        {
            toolTipButton.alpha = 0;
            toolTipCloseTper.alpha = 0;
        }
    }
}
