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
    private Vector3 offset;
    private string objectText;

    //usado si queremos enviarle un text por parametro dinamico
    public string ObjectText { get => objectText; set => objectText = value; }

    // Start is called before the first frame update
    void Start()
    {
        //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
        trigger = gameObject.AddComponent<EventTrigger>() as EventTrigger;

        //llama directamente a la clase estatica de cada tooltip correspondiente, cada objeto tooltip tiene su propia clase que maneja sus atributos
        switch (toolTipType)
        {
            case TypeToolTip.mnuAtom:
                //parametro manual que tenemos que calcular para que el tooltip se muestre a un delta del gameobject 
                //utilizamos el valor del screen para obtener la referencia de resolucion, sino queda mal posicionado.       
                offset = new Vector3(0.037f * Screen.width, 0, 0);
                setPointersToolTipStaticMove("* ÁTOMOS\n* TABLA PERIÓDICA", offset);
                break;

            case TypeToolTip.mnuCombineProton:
                //parametro manual que tenemos que calcular para que el tooltip se muestre a un delta del gameobject 
                //utilizamos el valor del screen para obtener la referencia de resolucion, sino queda mal posicionado.       
                offset = new Vector3(0.018f * Screen.width, 0.105f * Screen.height, 0);
                setPointersToolTipStaticMove("COMBINAR\n * ELEMENTOS\n * MOLECÚLAS \n * MATERIALES", offset);
                break;

            case TypeToolTip.mnuInfo:
                //parametro manual que tenemos que calcular para que el tooltip se muestre a un delta del gameobject 
                //utilizamos el valor del screen para obtener la referencia de resolucion, sino queda mal posicionado.       
                offset = new Vector3(-0.12f * Screen.width, 0.104f * Screen.height, 0);
                setPointersToolTipStaticMove("COMBINACIONES RECURRENTES\nINFORMACIÓN ADICIONAL", offset);
                break;

            case TypeToolTip.mnuMolecule:
                //parametro manual que tenemos que calcular para que el tooltip se muestre a un delta del gameobject 
                //utilizamos el valor del screen para obtener la referencia de resolucion, sino queda mal posicionado.       
                offset = new Vector3(0.037f * Screen.width, 0, 0);             
                setPointersToolTipStaticMove("* MOLÉCULAS", offset);
                break;

            case TypeToolTip.mnuSuggestions:
                //parametro manual que tenemos que calcular para que el tooltip se muestre a un delta del gameobject 
                //utilizamos el valor del screen para obtener la referencia de resolucion, sino queda mal posicionado.       
                offset = new Vector3(-0.050f * Screen.width, 0, 0);
                setPointersToolTipStaticMove("* SUGERENCIAS", offset);
                break;

            case TypeToolTip.mnuMaterial:
                //parametro manual que tenemos que calcular para que el tooltip se muestre a un delta del gameobject 
                //utilizamos el valor del screen para obtener la referencia de resolucion, sino queda mal posicionado.       
                offset = new Vector3(0.037f * Screen.width, 0, 0);
                setPointersToolTipStaticMove("* MATERIALES", offset);
                break;

            /*Tooltips que se mueven segun over mouse*/
            case TypeToolTip.closeTper:        
                setPointersToolTipMove("CERRAR TABLA PERIODICA");
                break;

            case TypeToolTip.buttonTper:
                setPointersToolTipMove(objectText);          
                break;

            case TypeToolTip.buttonRefColors:
                setPointersToolTipMove("REFERENCIAS DE COLORES \nSEGUN CLASIFICACIÓN");
                break;

            case TypeToolTip.buttonRefParam:
                setPointersToolTipMove("PARÁMETROS MOSTRADOS \nEN CADA ELEMENTO");
                break;
        }
    }

    private void setPointersToolTipMove (string textToShowToolTip)
    {
        //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
        UIToolTipMove.ShowToolTipstaticPointerEnter(textToShowToolTip, gameObject, trigger);
        UIToolTipMove.HideToolTipstaticPointerExit(gameObject, trigger);
        //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
        UIToolTipMove.HideToolTipstaticPointerClick(gameObject, trigger);
    }

    private void setPointersToolTipStaticMove(string textToShowToolTip, Vector3 offset)
    {
        //setea metodos static para agregar tooltip (por cada! tool tip tiene que haber un script)
        UIToolTipStaticMove.ShowToolTipstaticPointerEnter(textToShowToolTip, offset, gameObject, trigger);
        UIToolTipStaticMove.HideToolTipstaticPointerExit(gameObject, trigger);
        //este tiene que estar porque si no se moveria el mouse y solo se hace click el popop seguiria mostrandose
        UIToolTipStaticMove.HideToolTipstaticPointerClick(gameObject, trigger);
    }

}
