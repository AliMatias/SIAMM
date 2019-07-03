using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetailInfoLoader : MonoBehaviour
{

    //panel padre (asignado por interfaz)
    public GameObject panel;
    //array de textos a modificar
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        //obtengo array de textos para después modificar en todos los hijos del panel padre busca
        //no importa si tengo como en este caso panel de panel...
        texts = panel.GetComponentsInChildren<TextMeshProUGUI>();   
    }


    //setea la info detallada AGREGADA en las hojas siguientes en el flip book
    public void SetDetailInfo(ElementInfoDetail elementInfoDetail)
    {
        foreach (TextMeshProUGUI a in texts)
        {
            Debug.Log("LLENO TEXTOS");

        }

    }

}
