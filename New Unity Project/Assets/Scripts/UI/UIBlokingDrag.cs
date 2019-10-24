using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UIBlokingDrag : MonoBehaviour,  IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect ScrollRectParent;

    public void Awake()
    {
        // find our ScrollRect parent
        if (GetComponentInParent<ScrollRect>() != null)
            ScrollRectParent = GetComponentInParent<ScrollRect>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.pointerDrag = null;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.pointerDrag = null;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.pointerDrag = null;
    }
}




