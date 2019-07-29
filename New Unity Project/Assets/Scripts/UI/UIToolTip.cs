using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToolTip : MonoBehaviour
{

    public Text tooltipText;
    public CanvasGroup uiElement;

    // public Transform popupText;
    // public static string statusText = "off";

    // void OnMouseEnter()
    // {
    //     if (statusText == "off")   
    //     {
    //         popupText.GetComponent<TextMesh>().text = "NOMBRE ELEMENTO";
    //         statusText = "on";
    //         Instantiate(popupText, new Vector3(transform.position.x, transform.position.y + 2, 0), popupText.rotation);
    //     }
    // }

    //void OnMouseExit()
    // {
    //     if (statusText == "on")
    //     {
    //         statusText = "off";
    //     }
    // }

    void Update()
    {
        if (!uiElement.gameObject.activeSelf)
            tooltipText.text = "";

    }

    //void OnGUI()
    //{
    //    // This box is larger than many elements following it, and it has a tooltip.
    //    GUI.Box(new Rect(5, 35, 110, 75), new GUIContent("Box", "this box has a tooltip"));

    //    // This button is inside the box, but has no tooltip so it does not
    //    // override the box's tooltip.
    //    GUI.Button(new Rect(10, 55, 100, 20), "No tooltip here");

    //    // This button is inside the box, and HAS a tooltip so it overrides
    //    // the tooltip from the box.
    //    GUI.Button(new Rect(10, 80, 100, 20), new GUIContent("I have a tooltip", "The button overrides the box"));

    //    // finally, display the tooltip from the element that has
    //    // mouseover or keyboard focus
    //    GUI.Label(new Rect(10, 40, 100, 40), GUI.tooltip);
    //}

}
