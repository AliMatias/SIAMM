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
    public GameObject panelMateriales;
    public GameObject panelIsotopos;
    //array de textos a modificar
    private TextMeshProUGUI[] textsElementos;
    private TextMeshProUGUI[] textsMol;
    private TextMeshProUGUI[] textsMat;
    private TextMeshProUGUI[] textsIsotopos;

    #endregion

    private void Awake()
    {
        //obtengo array de textos para después modificar en todos los hijos del panel padre busca
        //no importa si tengo como en este caso panel de panel...
        textsElementos = panelElementos.GetComponentsInChildren<TextMeshProUGUI>();
        textsMol = panelMoleculas.GetComponentsInChildren<TextMeshProUGUI>();
        textsMat = panelMateriales.GetComponentsInChildren<TextMeshProUGUI>();
        textsIsotopos = panelIsotopos.GetComponentsInChildren<TextMeshProUGUI>();
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
                a.text = "Nomenclatura de Stock: " + managerNullables(molInfo.StockNomenclature);
            if (a.name == "txtNomenclaturaTradicional")
                a.text = "Nomenclatura Tradicional: " + managerNullables(molInfo.TraditionalNomenclature);

            if (a.name == "txtCaracteristicas")
                a.text = "Caracteristicas: \n" + managerNullables(molInfo.Caracteristicas);
            if (a.name == "txtPropiedades")
                a.text = "Propiedades: \n" + managerNullables(molInfo.Propiedades);
            if (a.name == "txtUsos")
                a.text = "Usos: \n" + managerNullables(molInfo.Usos);
            if (a.name == "txtClasificacion")
                a.text = "Clasificación: \n" + managerNullables(molInfo.Clasificacion);

        }
    }

    //setea la info básica y muestra el panel de materiales
    public void SetPanelInfoMaterial(MaterialData matInfo)
    {
        foreach (TextMeshProUGUI a in textsMat)
        {
            if (a.name == "txtCaracteristicas")
                a.text = "Caracteristicas: \n" + managerNullables(matInfo.Caracteristicas);
            if (a.name == "txtPropiedades")
                a.text = "Propiedades: \n" + managerNullables(matInfo.Propiedades);
            if (a.name == "txtUsos")
                a.text = "Usos: \n" + managerNullables(matInfo.Usos);
            if (a.name == "txtClasificacion")
                a.text = "Clasificación: \n" + managerNullables(matInfo.Clasificacion);
            if (a.name == "txtNotas")
                a.text = "Resumen: \n" + managerNullables(matInfo.Notas);
        }
    }


    //setea la info básica y muestra el panel de Isotopos
    public void SetPanelInfoIsotopos(IsotopoAllData isotopoInfo)
    {
        foreach (TextMeshProUGUI a in textsIsotopos)
        {
            if (a.name == "txtIsotopo")
                a.text = "Nombre Isótopo:: " + managerNullables(Convert.ToString(isotopoInfo.Isotopo));
            if (a.name == "txtNumeroAtomico")
                a.text = "Número Átomico: " + managerNullables(Convert.ToString(isotopoInfo.NumeroAtomico));
            if (a.name == "txtnumeroCorrelativo")
                a.text = "Número Correlativo: " + managerNullables(Convert.ToString(isotopoInfo.NumeroCorrelativo));
            if (a.name == "txtNumeroMasa")
                a.text = "Número Masa: " + managerNullables(Convert.ToString(isotopoInfo.NumeroMasa));
            if (a.name == "txtMasaAtomicaRelativa")
                a.text = "Masa Atómica Relativa: " + managerNullables(isotopoInfo.MasaAtomicaRelativa);
            if (a.name == "txtcomposicionIsotopica")
                a.text = "Composición Isotópica: " + managerNullables(isotopoInfo.ComposicionIsotopica);
            if (a.name == "txtPesoAtomicoEstandar")
                a.text = "Peso Atómico Estandar: " + managerNullables(isotopoInfo.PesoAtomicoEstandar);
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
