using TMPro;
using UnityEngine;

public class ElementName : MonoBehaviour
{
    private TextMeshProUGUI elementName;

    //cambiar lo que dice el label
    public void ChangeElement(string name)
    {
        Debug.Log(name);
        Debug.Log(elementName.text);
        elementName.SetText(name);
    }
}
