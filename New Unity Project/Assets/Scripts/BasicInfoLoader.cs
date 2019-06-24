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
        //obtengo array de textos para después modificar en todos los hijos del panel padre busca
        //no importa si tengo como en este caso panel de panel...
       texts = panel.GetComponentsInChildren<TextMeshProUGUI>();
    }

    //setea la info básica y muestra el panel
    public void SetBasicInfo(ElementInfoBasic elementInfoBasic)
    {
        foreach (TextMeshProUGUI a in texts)
        {

            if (a.name == "txtNombre")
                a.text = elementInfoBasic.Name;
            if (a.name == "txtNroAtomico")
            {
                a.text = "Nro atómico: " + elementInfoBasic.Nroatomico;
                //aca cargaria la foto del elemento
            }
            if (a.name == "txtSimbol")
                a.text = "Símbolo: " + elementInfoBasic.Simbol;
            if (a.name == "txtPeso")
                a.text = "Peso atómico: " + elementInfoBasic.PesoAtomico;
            if (a.name == "txtPeriodo")
                a.text = "Período: " + elementInfoBasic.Periodo;
            if (a.name == "txtColor")
                a.text = "Color: " + elementInfoBasic.Color;
            if (a.name == "txtClasificacion")
                a.text = "Clasificación: " + elementInfoBasic.Clasificacion;
            if (a.name == "txtClasificacionGrupo")
                a.text = "Clasificación Grupo: " + elementInfoBasic.Clasificacion_grupo;
            if (a.name == "txtValencia")
                a.text = "Valencia: " + elementInfoBasic.Valencia;
            if (a.name == "txtEstadoNatural")
                a.text = "Estado Natural: " + elementInfoBasic.Estado_natural;
            if (a.name == "txtEstructuraCrist")
            {
                a.text = "Estructura Cristalina: " + elementInfoBasic.EstructuraCristalina;
                //aca luego cargaria la foto..
            }
            if (a.name == "txtNroOxi")
                a.text = "Números Oxidación: " + elementInfoBasic.NumerosOxidacion;
            if (a.name == "txtConfElectronica")
                a.text = "Configuración Electrónica: " + elementInfoBasic.ConfElectronica;
            if (a.name == "txtPtoFusion")
                a.text = "Punto de Fusión: " + elementInfoBasic.PuntoFusion;
            if (a.name == "txtPtoEbullicion")
                a.text = "Punto de Ebullicón: " + elementInfoBasic.PuntoEbullicion;
            if (a.name == "txtResumen")
                a.text = elementInfoBasic.Resumen;

        }
        panel.SetActive(true);
    }

    //Cierra panel de info básica
    public void CloseBasicInfoPanel()
    {
        panel.SetActive(false);
    }
}
