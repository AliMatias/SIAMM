using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIToolTipStaticMove : MonoBehaviour
{
    private Text tooltipText;
    public CanvasGroup uiElement;
    public RectTransform background;
    private static UIToolTipStaticMove instate;

    private void Awake()
    {
        instate = this;
        background = transform.Find("BackGround").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
    }

    private void ShowToolTip(string text, Vector3 offset, GameObject elem)
    {
        background.gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = text;
        float paddingtextSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingtextSize * 2f, tooltipText.preferredHeight + paddingtextSize * 2f);
        background.sizeDelta = backgroundSize;
        transform.position = elem.transform.position + offset;//del game object que aplicara el tooltip siempre un delta!
    }

    private void HideToolTip()
    {
        background.gameObject.SetActive(false);
        uiElement.alpha = 0;
    }

    //a diferencia del tipo statico, este recibe un offset que sirve para calcular del punto del go de referncia donde coloca el tooltip
    public static void ShowToolTipstaticPointerEnter(string text, Vector3 offset, GameObject elem, EventTrigger trigger)
    {     
        trigger = elem.gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { instate.ShowToolTip(text, offset, elem); });
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
