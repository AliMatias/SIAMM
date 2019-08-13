using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadPopupBtn : MonoBehaviour
{
    //se setea desde la interface identificando cual es el boton que tendra la interaccion del tooltip de acuerdo al tipo
    public GameObject menuSet= null;
    // seteo desde inspector a que label pertenece la instancia de este script
    [SerializeField]
    public TypeToolTip toolTipType;
    private EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
        trigger = menuSet.gameObject.AddComponent<EventTrigger>() as EventTrigger;

        //llama directamente a la clase estatica de cada tooltip correspondiente, cada objeto tooltip tiene su propia clase que maneja sus atributos
        switch (toolTipType)
        {
            case TypeToolTip.mnuAtom:                        
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipAtom.ShowToolTipstaticPointerEnter("* ÁTOMOS\n* TABLA PERIÓDICA", menuSet, trigger);
                UIToolTipAtom.HideToolTipstaticPointerExit(menuSet, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipAtom.HideToolTipstaticPointerClick(menuSet, trigger);
                break;
            case TypeToolTip.mnuCombineProton:
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipCombine.ShowToolTipstaticPointerEnter("COMBINAR\n * ELEMENTOS\n * MOLECÚLAS \n * MATERIALES", menuSet, trigger);
                UIToolTipCombine.HideToolTipstaticPointerExit(menuSet, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipCombine.HideToolTipstaticPointerClick(menuSet, trigger);
                break;
            case TypeToolTip.mnuInfo:
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipInfo.ShowToolTipstaticPointerEnter("SUGERENCIAS\nINFORMACIÓN ADICIONAL", menuSet, trigger);
                UIToolTipInfo.HideToolTipstaticPointerExit(menuSet, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipInfo.HideToolTipstaticPointerClick(menuSet, trigger);
                break;
            case TypeToolTip.mnuMolecule:
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipMolecule.ShowToolTipstaticPointerEnter("* MOLÉCULAS", menuSet, trigger);
                UIToolTipMolecule.HideToolTipstaticPointerExit(menuSet, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipMolecule.HideToolTipstaticPointerClick(menuSet, trigger);
                break;
        }
    }
}
