using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIToolTipText : MonoBehaviour
{
    public CanvasGroup uiElement;
    private Text tooltipText;
    private static UIToolTipText instate;

    private void Awake()
    {
        instate = this;
        tooltipText = transform.Find("Text").GetComponent<Text>();
    }

    //Callback function delegada que ejecutara el trigger event
    private void ShowTextToolTip()
    {
        gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = "Click Izquierdo: Agregar Átomo \n\n";
        tooltipText.text = tooltipText.text + "Click Derecho: Informacion del Átomo";
    }

    //Callback function delegada que ejecutara el trigger event
    private void ExitTextToolTip()
    {
        gameObject.SetActive(false);
        uiElement.alpha = 0;
    }


    public static void showToolTipstaticPointerEnter(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { instate.ShowTextToolTip(); });
        trigger.triggers.Add(entry);
    }

    public static void hideToolTipstaticPointerExit(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => { instate.ExitTextToolTip(); });
        trigger.triggers.Add(entry);
    }

    public static void hideToolTipstaticPointerClick(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { instate.ExitTextToolTip(); });
        trigger.triggers.Add(entry);
    }
}
