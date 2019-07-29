using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITooltipPrefab : MonoBehaviour
{
    private Text tooltipText;
    public RectTransform background;
    public CanvasGroup uiElement;
    private static UITooltipPrefab instate;
    private Vector3 offset = new Vector3 (1,1,0);

    private void Awake()
    {
        instate = this;
        background = transform.Find("BackGround").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
    }

    //hace que se mueva el tooltip en conjunto con la reference camera
    void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    private void showToolTip(string text)
    {
        background.gameObject.SetActive(true);
        uiElement.alpha = 1;
        tooltipText.text = text;
        float paddingtextSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingtextSize * 2f, tooltipText.preferredHeight + paddingtextSize * 2f);
        background.sizeDelta = backgroundSize;       
    }

    private void hideToolTip()
    {
        background.gameObject.SetActive(false);
        uiElement.alpha = 0;
    }

    public static void showToolTipstatic(string text)
    {
        instate.showToolTip(text);
    }

    public static void hideToolTipstatic()
    {
        instate.hideToolTip();
    }

}
