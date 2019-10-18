using UnityEngine;
using UnityEngine.UI;

public class ChangeTextProperties : MonoBehaviour
{
    private Color originalColor;
    public Color highlightColor;
    void Start()
    {
        originalColor = gameObject.GetComponent<Text>().color;
    }

    public void HoverText()
    {
        gameObject.GetComponent<Text>().color = highlightColor;
    }

    public void StopHoverText()
    {
        gameObject.GetComponent<Text>().color = originalColor;
    }
}
