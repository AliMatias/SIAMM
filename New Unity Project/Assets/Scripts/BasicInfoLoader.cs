using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*Carga la info básica de los elementos*/
public class BasicInfoLoader : MonoBehaviour
{
    //panel padre (asignado por interfaz)
    public GameObject panel;
    //array de textos a modificar
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        //obtengo array de textos para después modificar
       texts = panel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    //setea la info básica y muestra el panel
    public void SetBasicInfo(ElementInfoBasic elementInfoBasic)
    {
        //acá están hardcodeadas las posiciones, ver si hay una manera mas linda de hacerlo
        texts[0].text = "Nombre: " + elementInfoBasic.Name;
        texts[1].text = "Nro atómico: " + elementInfoBasic.Nroatomico;
        texts[2].text = "Símbolo: " + elementInfoBasic.Simbol;
        texts[3].text = "Peso atómico: " + elementInfoBasic.PesoAtomico;
        texts[4].text = "Período: " + elementInfoBasic.Periodo;
        texts[5].text = "Color: " + elementInfoBasic.Color;

        panel.SetActive(true);
    }

    //Cierra panel de info básica
    public void CloseBasicInfoPanel()
    {
        panel.SetActive(false);
    }
}
