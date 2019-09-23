using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/*Carga la info básica de los elementos*/
public class PanelInfoLoader : MonoBehaviour
{
    #region atributos
    //panel padre (asignado por interfaz)
    public GameObject panelElementos;
    public GameObject panelMoleculas;
    //array de textos a modificar
    private TextMeshProUGUI[] textsElementos;
    private TextMeshProUGUI[] textsMol;

    #endregion

    private void Awake()
    {
        //obtengo array de textos para después modificar en todos los hijos del panel padre busca
        //no importa si tengo como en este caso panel de panel...
        textsElementos = panelElementos.GetComponentsInChildren<TextMeshProUGUI>();
        textsMol = panelMoleculas.GetComponentsInChildren<TextMeshProUGUI>();
    }

    #region Metodos 

    //setea la info básica y muestra el panel
    public void SetPanelInfoElement(ElementInfoPanelInfo elementInfoBasic)
    {
        foreach (TextMeshProUGUI a in textsElementos)
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


    //setea la info básica y muestra el panel
    public void SetPanelInfoMolecule(MoleculeData molInfo)
    {
        foreach (TextMeshProUGUI a in textsMol)
        {
            if (a.name == "txtFormula")
                a.text = "Fórmula: " + managerNullables(molInfo.Formula);
            if (a.name == "txtNomenclaturaSistematica")
                a.text = "Nomenclatura Sistemática: " + managerNullables(molInfo.SystematicNomenclature);
            if (a.name == "txtnomenclaturaStock")
                a.text = "Nomenclatura de Stock:" + managerNullables(molInfo.StockNomenclature);
            if (a.name == "txtNomenclaturaTradicional")
                a.text = "Nomenclatura Tradicional: " + managerNullables(molInfo.TraditionalNomenclature);
            if (a.name == "txtCaracteristicas")
                a.text = "Caracteristicas: " + managerNullables(molInfo.Caracteristicas);
            if (a.name == "txtPropiedades")
                a.text = "Propiedades: " + managerNullables(molInfo.Propiedades);
            if (a.name == "txtUsos")
                a.text = "Usos: " + managerNullables(molInfo.Usos);
            if (a.name == "txtClasificacion")
                a.text = "Clasificación:" + managerNullables(molInfo.Clasificacion);
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
