using TMPro;
using UnityEngine;

public class ElementName : MonoBehaviour
{
    private TextMesh elementName;

    //cambiar lo que dice el label
    public void ChangeElement(string name)
    {
        Debug.Log(name);
        Debug.Log(elementName.text);
        elementName.text = name;
    }
}
