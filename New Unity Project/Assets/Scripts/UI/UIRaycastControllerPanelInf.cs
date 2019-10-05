using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycastControllerPanelInf : MonoBehaviour, IPointerClickHandler
{
    private GameObject panelPadre;
    private CanvasGroup[] panelColection;


    private void Awake()
    {
        panelColection = panelPadre.GetComponents<CanvasGroup>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        // This will only execute if the objects collider was the first hit by the click's raycast
        Debug.Log(gameObject.name + ": I was clicked!");

        foreach (CanvasGroup tab in panelColection)
        {
            //if (tab.gameObject.name == "Content1")
            //   tab.alpha = 1;
            //else
            //    tab.alpha = 0;
            Debug.Log(tab.gameObject.name);
        }
    }

}
