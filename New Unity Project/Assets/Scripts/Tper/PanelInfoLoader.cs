using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/*Carga la info básica de los elementos*/
public class PanelInfoLoader : MonoBehaviour
{
    #region atributos
    //panel padre (asignado por interfaz)
    public GameObject panel;
    //array de textos a modificar
    private TextMeshProUGUI[] texts;

    #endregion

    private void Awake()
    {
        //obtengo array de textos para después modificar en todos los hijos del panel padre busca
        //no importa si tengo como en este caso panel de panel...
        texts = panel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    #region Metodos 

    //setea la info básica y muestra el panel
    public void SetPanelInfo(ElementInfoPanelInfo elementInfoBasic)
    {
        foreach (TextMeshProUGUI a in texts)
        {
            if (a.name == "txtClasificacion")
                a.text = "Clasificación: " + elementInfoBasic.Clasificacion;
            if (a.name == "txtClasificacionGrupo")
                a.text = "Clasificación Grupo: " + elementInfoBasic.Clasificacion_grupo;        
            if (a.name == "txtNroOxidacion")
                a.text = "Números Oxidación: " + managerNullables(elementInfoBasic.NumerosOxidacion);        
            if (a.name == "txtPtoFusion")
                a.text = "Punto de Fusión: " + managerNullables(elementInfoBasic.PuntoFusion);
            if (a.name == "txtPtoEbullicion")
                a.text = "Punto de Ebullición: " + managerNullables(elementInfoBasic.PuntoEbullicion);
            if(a.name == "txtDistribucionNiveles")
                a.text = "Distribución de Electrones en Niveles: " + managerNullables(elementInfoBasic.DistribucionDeelectrones);
        }
    }


    private string managerNullables(String valor)
    {
        if (valor == null || valor == "" || valor == string.Empty)
            return "n/a";
        return valor;
    }

    #endregion
}
