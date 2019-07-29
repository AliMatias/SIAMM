using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

//Carga tabla periódica
public class LoadTper : MonoBehaviour
{

    #region atributos
    private Button button;
    private QryElementos qryElement;
    private GridLayoutGroup glg;
    private UIPopup popup;
    public Text toolTipText;//viene el canvas principal
    private EventTrigger trigger;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //instancio la clase de metodos sobre la base de datos
        GameObject go = new GameObject();
        go.AddComponent<QryElementos>();
        qryElement = go.GetComponent<QryElementos>();

        glg = this.GetComponent<GridLayoutGroup>();

        popup = FindObjectOfType<UIPopup>();
     
        //Recorro todas las celdas que tienen un game object
        for (int i = 0; i < glg.transform.childCount; i++)
        {
            button = glg.transform.GetChild(i).GetComponent<Button>();

            //si no es NULL quiere decir que MAPEO un boton ahi tengo que ir a la base de datos
            if (button != null)
            {
                LoadData(button);
            }
        }
    }

    #region Metodos
    /*Metodo para el seteo de los objetos TEXT de cada boton de la tabla periodica*/
    private void LoadData (Button elem)
    {
        ElementTabPer element = new ElementTabPer();
      
        //obtiene datos del elemento según cantidad de protones
        try
        {
            element = qryElement.GetElementFromNro(getNroAtomicoId(elem));
        }
        catch (Exception e)
        {
            Debug.LogError("LoadTPer :: Ocurrio un error al buscar Elemento desde Identificador: " + e.Message);
            popup.MostrarPopUp("Elementos Qry DB", "Error obteniendo Elemento desde Identificador");
            return;
        }

        //obtengo la lista de objetos o coleccion de objetos de tipo TEXT que estan en los botones
        Text[] textosObj = elem.GetComponentsInChildren<Text>();
       
        //recorro todos los game object que contiene el boton, se podria hacer por orden de objetos, como estan creados en el boton
        for (int j = 0; j < textosObj.Length; j++)
        {

            if (textosObj[j].name == "txtDistElect")
                textosObj[j].text = managerNullables(element.ConfElectronica);
            if (textosObj[j].name == "txtPeso")
                textosObj[j].text = Convert.ToString(element.PesoAtomico);
            if (textosObj[j].name == "txtNombre")
                textosObj[j].text = element.Simbol;
        }

        //a cada boton le voy a agregar componentes que estan por fuera del prefab para el manejo de tooltips
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        AddEventTriggerListener(trigger, EventTriggerType.PointerEnter, ShowTextToolTip);
        AddEventTriggerListener(trigger, EventTriggerType.PointerExit, ExitTextToolTip);
    }


    //Callback function delegada que ejecutara el trigger event
    void ShowTextToolTip(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Debug.Log("Click: pointerEventData=" + pointerEventData);

        toolTipText.text = "Click Izquierdo: Agregar Átomo \n\n";
        toolTipText.text = toolTipText.text + "Click Derecho: Informacion del Átomo";
    }

    //Callback function delegada que ejecutara el trigger event
    void ExitTextToolTip(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Debug.Log("Click: pointerEventData=" + pointerEventData);

        toolTipText.text = "";
    }

    public static void AddEventTriggerListener(EventTrigger trigger,  EventTriggerType eventType, System.Action<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        trigger.triggers.Add(entry);
    }

    /*Obtiene el nro atomico a partir del "string" que tiene en el boton como txtNroAtomico*/
    public int getNroAtomicoId (Button elem)
    {
        int nroAtomico = 0;

        Text[] textosObj = elem.GetComponentsInChildren<Text>();

        for (int j = 0; j < textosObj.Length; j++)
        {
            if (textosObj[j].name == "txtNroAtomico")         
                nroAtomico = Convert.ToInt32(textosObj[j].text);        
        }

        return nroAtomico;
    }

    //trae de la DB la info básica
    public ElementInfoBasic LoadInfoBasica(int nroAtomico)
    {
        ElementInfoBasic elementInfoBasic = new ElementInfoBasic();
        try
        {
            elementInfoBasic = qryElement.GetElementInfoBasica(nroAtomico);
        }
        catch (Exception e)
        {
            Debug.LogError("LoadTper :: Ocurrio un error al buscar Informacion Basica: " + e.Message);
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Informacion Basica de Elementos Quimicos");
        }

        return elementInfoBasic;
    }

    //trae de la DB la info detallada que complementa a la basica
    public ElementInfoDetail LoadInfoDeatail(int nroAtomico)
    {
        ElementInfoDetail elementInfoDetail = new ElementInfoDetail();    
        try
        {
            elementInfoDetail = qryElement.GetElementInfoDetail(nroAtomico);
        }
        catch (Exception e)
        {
            Debug.LogError("LoadTper :: Ocurrio un error al buscar Informacion Detalla: " + e.Message);
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Informacion Detallada de Elementos Quimicos");
        }

        return elementInfoDetail;
    }

    private string managerNullables(String valor)
    {
        if (valor == null || valor == "" || valor == string.Empty)
            return "n/a";
        return valor;
    }
    #endregion
}
