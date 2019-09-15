using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIToolTipText : MonoBehaviour
{
    private Text tooltipText;
    public CanvasGroup uiElement;
    private static UIToolTipText instate;

    private void Awake()
    {
        //instate = this;
        //tooltipText = transform.Find("Text").GetComponent<Text>();
    }

    //Callback function delegada que ejecutara el trigger event
    private void ShowTextToolTip()
    {
        //gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = "Click Izquierdo: Agregar Átomo \n\n";
        tooltipText.text = tooltipText.text + "Click Derecho: Informacion del Átomo";
        //UIToolTipControl.flagTooltip = true;//le digo al controlador que se activa un tooltip
    }

    //Callback function delegada que ejecutara el trigger event
    private void ExitTextToolTip()
    {
        //gameObject.SetActive(false);
        uiElement.alpha = 0;
        //UIToolTipControl.flagTooltip = false;//le digo al controlador que se activa un tooltip
    }


    public static void ShowToolTipstaticPointerEnter(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { instate.ShowTextToolTip(); });
        trigger.triggers.Add(entry);
    }

    public static void HideToolTipstaticPointerExit(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => { instate.ExitTextToolTip(); });
        trigger.triggers.Add(entry);
    }

    public static void HideToolTipstaticPointerClick(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { instate.ExitTextToolTip(); });
        trigger.triggers.Add(entry);
    }
}
