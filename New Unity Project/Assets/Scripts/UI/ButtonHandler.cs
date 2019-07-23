using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//Manejador de eventos del mouse
public class ButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onLeft;
    public UnityEvent onRight;
    public UnityEvent onMiddle;

    //Según el botón apretado del mouse llama al evento indicado
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
