using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadPopupBtn : MonoBehaviour
{
    public Button btnCombine = null;
    public GameObject mnuMolecule = null;
    public GameObject mnuInfo = null;
    public GameObject mnuAtom = null;
    private EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {    
        if (mnuAtom != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = mnuAtom.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipAtom.ShowToolTipstaticPointerEnter("* ÁTOMOS\n* TABLA PERIÓDICA", mnuAtom, trigger);
            UIToolTipAtom.HideToolTipstaticPointerExit(mnuAtom, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipAtom.HideToolTipstaticPointerClick(mnuAtom, trigger);
        }

        else if (btnCombine != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = btnCombine.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipCombine.ShowToolTipstaticPointerEnter("COMBINAR\n * ELEMENTOS\n * MOLECÚLAS \n * MATERIALES",btnCombine, trigger);
            UIToolTipCombine.HideToolTipstaticPointerExit(btnCombine, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipCombine.HideToolTipstaticPointerClick(btnCombine, trigger);
        }

        else if (mnuMolecule != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = mnuMolecule.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipMolecule.ShowToolTipstaticPointerEnter("* MOLÉCULAS", mnuMolecule, trigger);
            UIToolTipMolecule.HideToolTipstaticPointerExit(mnuMolecule, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipMolecule.HideToolTipstaticPointerClick(mnuMolecule, trigger);
        }

        else if (mnuInfo != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = mnuInfo.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipInfo.ShowToolTipstaticPointerEnter("SUGERENCIAS\nINFORMACIÓN ADICIONAL", mnuInfo, trigger);
            UIToolTipInfo.HideToolTipstaticPointerExit(mnuInfo, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipInfo.HideToolTipstaticPointerClick(mnuInfo, trigger);
        }
    }

}
