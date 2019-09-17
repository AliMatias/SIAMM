using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToolTipControl : MonoBehaviour
{
    public CanvasGroup panelTper;
    public CanvasGroup toolTipCanvas;
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
        if (!panelTper.gameObject.activeSelf)
        {
            toolTipCanvas.alpha = 0;
        }

        if (!flagTooltip)
        {
            toolTipCanvas.alpha = 0;
        }

    }
}
