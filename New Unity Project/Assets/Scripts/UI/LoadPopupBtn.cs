using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadPopupBtn : MonoBehaviour
{
    // seteo desde inspector a que label pertenece la instancia de este script
    [SerializeField]
    public TypeToolTip toolTipType;
    private EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
        trigger = gameObject.AddComponent<EventTrigger>() as EventTrigger;

        //llama directamente a la clase estatica de cada tooltip correspondiente, cada objeto tooltip tiene su propia clase que maneja sus atributos
        switch (toolTipType)
        {
            case TypeToolTip.mnuAtom:                        
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipAtom.ShowToolTipstaticPointerEnter("* ÁTOMOS\n* TABLA PERIÓDICA", gameObject, trigger);
                UIToolTipAtom.HideToolTipstaticPointerExit(gameObject, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipAtom.HideToolTipstaticPointerClick(gameObject, trigger);
                break;
            case TypeToolTip.mnuCombineProton:
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipCombine.ShowToolTipstaticPointerEnter("COMBINAR\n * ELEMENTOS\n * MOLECÚLAS \n * MATERIALES", gameObject, trigger);
                UIToolTipCombine.HideToolTipstaticPointerExit(gameObject, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipCombine.HideToolTipstaticPointerClick(gameObject, trigger);
                break;
            case TypeToolTip.mnuInfo:
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipInfo.ShowToolTipstaticPointerEnter("SUGERENCIAS\nINFORMACIÓN ADICIONAL", gameObject, trigger);
                UIToolTipInfo.HideToolTipstaticPointerExit(gameObject, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipInfo.HideToolTipstaticPointerClick(gameObject, trigger);
                break;
            case TypeToolTip.mnuMolecule:
                //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
                UIToolTipMolecule.ShowToolTipstaticPointerEnter("* MOLÉCULAS", gameObject, trigger);
                UIToolTipMolecule.HideToolTipstaticPointerExit(gameObject, trigger);
                //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
                UIToolTipMolecule.HideToolTipstaticPointerClick(gameObject, trigger);
                break;
        }
    }
}
