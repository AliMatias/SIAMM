using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicInfoLoader : MonoBehaviour
{
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        texts = this.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void SetBasicInfo(ElementInfoBasic elementInfoBasic)
    {
        texts[0].text = "Nombre: " + elementInfoBasic.Name;
        texts[2].text = "Símbolo: " + elementInfoBasic.Simbol;
        Debug.Log(texts.Length);
    }
}
