using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITooltip : MonoBehaviour
{
    private Text tooltipText;
    public RectTransform background;
    public CanvasGroup uiElement;
    private static UITooltip instate;
    private Vector3 offset = new Vector3 (1,1,0);

    private void Awake()
    {
        instate = this;
        background = transform.Find("BackGround").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
    }

    //hace que se mueva el tooltip en conjunto con la reference camera
    void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    private void showToolTip(string text)
    {
        background.gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = text;
        float paddingtextSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingtextSize * 2f, tooltipText.preferredHeight + paddingtextSize * 2f);
        background.sizeDelta = backgroundSize;       
    }

    private void hideToolTip()
    {
        background.gameObject.SetActive(false);
        uiElement.alpha = 0;
    }

    //a diferencia del tipo text, este recibe un texto DINAMICO para colocar en el tooltip
    public static void showToolTipstaticPointerEnter(string text, Button elem, EventTrigger trigger)
    {     
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { instate.showToolTip(text); });
        trigger.triggers.Add(entry);
    }

    public static void hideToolTipstaticPointerExit(Button elem, EventTrigger trigger)
    {      
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => { instate.hideToolTip(); });
        trigger.triggers.Add(entry);
    }

    public static void hideToolTipstaticPointerClick(Button elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { instate.hideToolTip(); });
        trigger.triggers.Add(entry);
    }
}
