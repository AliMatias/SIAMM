using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIToolTipMove : MonoBehaviour
{
    private Text tooltipText;
    public CanvasGroup uiElement;
    public RectTransform background;
    private static UIToolTipMove instate;
    private Vector3 offset;

    private void Awake()
    {
        instate = this;
        background = transform.Find("BackGround").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
        offset = new Vector3(1, 1, 0);//este atributo es estatico
    }

    //hace que se mueva el tooltip en conjunto con la reference camera
    void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    //tener en cuenta que es para la tabla periodica en caso de necesitar otro, hacer overrride
    private void ShowToolTip(string text)
    {
        background.gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = text;
        float paddingtextSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingtextSize * 2f, tooltipText.preferredHeight + paddingtextSize * 2f);
        background.sizeDelta = backgroundSize;
        UIToolTipControl.flagTooltip = true;//le digo al controlador que se activa un tooltip DE LA TABLA! PERIODICA  
    }

    //tener en cuenta que es para la tabla periodica en caso de necesitar otro, hacer overrride
    private void HideToolTip()
    {
        background.gameObject.SetActive(false);
        uiElement.alpha = 0;
        UIToolTipControl.flagTooltip = false;//le digo al controlador que se activa un tooltip DE LA TABLA! PERIODICA  
    }

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
