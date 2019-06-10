    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;


using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler
{

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Left)
    //        Debug.Log("Left click");
    //    else if (eventData.button == PointerEventData.InputButton.Middle)
    //        Debug.Log("Middle click");
    //    else if (eventData.button == PointerEventData.InputButton.Right)
    //        Debug.Log("Right click");

    //}


    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onMiddle;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeft.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRight.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            onMiddle.Invoke();
        }
    }
}
