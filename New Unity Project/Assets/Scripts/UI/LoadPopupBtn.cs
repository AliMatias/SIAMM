using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadPopupBtn : MonoBehaviour
{
    public Button btnAtom = null;
    public Button btnTPer = null;
    public Button btnCombine = null;
    public Button btnMolecule = null;
    public Button btnInfo = null; 
    private EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        if (btnAtom != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = btnAtom.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipAtom.ShowToolTipstaticPointerEnter("ÁTOMOS", btnAtom, trigger);
            UIToolTipAtom.HideToolTipstaticPointerExit(btnAtom, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipAtom.HideToolTipstaticPointerClick(btnAtom, trigger);
        }

        else if (btnTPer != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = btnTPer.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipTper.ShowToolTipstaticPointerEnter("TABLA\nPERIÓDICA",btnTPer, trigger);
            UIToolTipTper.HideToolTipstaticPointerExit(btnTPer, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipTper.HideToolTipstaticPointerClick(btnTPer, trigger);
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

        else if (btnMolecule != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = btnMolecule.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipMolecule.ShowToolTipstaticPointerEnter("MOLÉCULAS",btnMolecule, trigger);
            UIToolTipMolecule.HideToolTipstaticPointerExit(btnMolecule, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipMolecule.HideToolTipstaticPointerClick(btnMolecule, trigger);
        }

        else if (btnInfo != null)
        {
            //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
            trigger = btnInfo.gameObject.AddComponent<EventTrigger>() as EventTrigger;
            //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
            UIToolTipInfo.ShowToolTipstaticPointerEnter("SUGERENCIAS\nINFORMACIÓN ADICIONAL", btnInfo, trigger);
            UIToolTipInfo.HideToolTipstaticPointerExit(btnInfo, trigger);
            //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
            UIToolTipInfo.HideToolTipstaticPointerClick(btnInfo, trigger);
        }
    }

}
