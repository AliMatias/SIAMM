﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIToolTipInfo : MonoBehaviour
{
    private Text tooltipText;
    public CanvasGroup uiElement;
    public RectTransform background;
    public GameObject btn;
    private static UIToolTipInfo instate;

    private void Awake()
    {
        instate = this;
        background = transform.Find("BackGround").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
    }


    private void ShowToolTip(string text)
    {
        background.gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = text;
        float paddingtextSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingtextSize * 2f, tooltipText.preferredHeight + paddingtextSize * 2f);
        background.sizeDelta = backgroundSize;
        Vector3 offset = new Vector3(-200, 40, 0);
        transform.position = btn.transform.position + offset;//del boton siempre un delta!
    }

    private void HideToolTip()
    {
        background.gameObject.SetActive(false);
        uiElement.alpha = 0;
    }

    //a diferencia del tipo text, este recibe un texto DINAMICO para colocar en el tooltip
    public static void ShowToolTipstaticPointerEnter(string text, GameObject elem, EventTrigger trigger)
    {     
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { instate.ShowToolTip(text); });
        trigger.triggers.Add(entry);
    }

    public static void HideToolTipstaticPointerExit(GameObject elem, EventTrigger trigger)
    {      
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => { instate.HideToolTip(); });
        trigger.triggers.Add(entry);
    }

    public static void HideToolTipstaticPointerClick(GameObject elem, EventTrigger trigger)
    {
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { instate.HideToolTip(); });
        trigger.triggers.Add(entry);
    }
}