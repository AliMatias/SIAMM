using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToolTip : MonoBehaviour
{
    public Text tooltipText;
    public CanvasGroup uiElement;

    void Update()
    {
        if (!uiElement.gameObject.activeSelf)
            tooltipText.text = "";
    }
}
