using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycastController : MonoBehaviour, IPointerClickHandler
{
    private void Awake()
    {
    }

    public void OnPointerClick(PointerEventData data)
    {
        // This will only execute if the objects collider was the first hit by the click's raycast
        Debug.Log(gameObject.name + ": I was clicked!");
    }

}
