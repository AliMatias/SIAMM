using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

    #region Metodos
    //setea la info detallada AGREGADA en las hojas siguientes en el flip book
    public void SetDetailInfo(ElementInfoDetail elementInfoDetail)
    {
        foreach (TextMeshProUGUI a in texts)
        {

            if (a.name == "txtIsotoposEstables")
                a.text = "Isótopos Estables: " + managerNullables(elementInfoDetail.isotopos_estables);

            if (a.name == "txtIsotoposAplicaciones")
                a.text = "Isótopos Aplicaciones: " + managerNullables(elementInfoDetail.isotopos_aplicaciones);

            if (a.name == "txtTipoElectrico")
                a.text = "Tipo Eléctrico: " + managerNullables(elementInfoDetail.tipo_electrico);

            if (a.name == "txtRadioactivo")
                a.text = "Radioactivo: " + managerNullables(elementInfoDetail.radiactivo);

            if (a.name == "txtAbundanciaCortezaTerrestre")
                a.text = "Abundancia en Corteza Terrestre %: " + managerNullables(elementInfoDetail.abundancia_corteza_terrestre);

            if (a.name == "txtDescubrimiento")
                a.text = "Descubrimiento: " + managerNullables(elementInfoDetail.descubrimiento);

            if (a.name == "txtDescubiertoPor")
                a.text = "Descubierto Por: " + managerNullables(elementInfoDetail.descubierto_por);

            if (a.name == "txtAnguloRed")
                a.text = "Ángulos de Red: " + managerNullables(elementInfoDetail.angulos_de_red);

            if (a.name == "txtVidaMedia")
                a.text = "Vida Media: " + managerNullables(elementInfoDetail.vida_media);

            if (a.name == "txtModCompresi")
                a.text = "Módulo Compresibilidad(Bulk): " + managerNullables(elementInfoDetail.modulo_compresibilidad);

            if (a.name == "txtDurezaBrinell")
                a.text = "Dureza Brinell: " + managerNullables(elementInfoDetail.dureza_brinell);

            if (a.name == "txtPresionCritica")
                a.text = "Presión Crítica: " + managerNullables(elementInfoDetail.presion_critica);

            if (a.name == "txtTempCritica")
                a.text = "Temperatura Crítica: " + managerNullables(elementInfoDetail.temperatura_critica);

            if (a.name == "txtCondElectrica")
                a.text = "Conductividad Eléctrica: " + managerNullables(elementInfoDetail.conductividad_electrica);

            if (a.name == "txtDensidad")
                a.text = "Densidad (kg/m³): " + managerNullables(elementInfoDetail.densidad);

            if (a.name == "txtRadioCovalente")
                a.text = "Radio Covalente: " + managerNullables(elementInfoDetail.radio_covalente);

            if (a.name == "txtAfinElect")
                a.text = "Afinidad Eléctronica: " + managerNullables(elementInfoDetail.afinidad_electronica);

            if (a.name == "txtPuntoCurie")
                a.text = "Punto Curie: " + managerNullables(elementInfoDetail.punto_curie);

            if (a.name == "txtModoDecai")
                a.text = "Modo Decaimiento: " + managerNullables(elementInfoDetail.modo_decaimiento);

            if (a.name == "txtElectronegatividad")
                a.text = "Electronegatividad: " + managerNullables(elementInfoDetail.electronegatividad);

            if (a.name == "txtDensidadLiquida")
                a.text = "Densidad Líquida: " + managerNullables(elementInfoDetail.densidadliquida);

            if (a.name == "txtConstanteRed")
                a.text = "Constante de Red: " + managerNullables(elementInfoDetail.constante_red);

            if (a.name == "txtMultiGas")
                a.text = "Multiplicidad Atomica del Gas: " + managerNullables(elementInfoDetail.multiplicidad_atomica_gas);

            if (a.name == "txtCalorFusion")
                a.text = "Calor de Fusión: " + managerNullables(elementInfoDetail.calor_de_fusion);

            if (a.name == "txtCalorVapor")
                a.text = "Calor de Vaporización: " + managerNullables(elementInfoDetail.calor_de_vaporizacion);

            if (a.name == "txtTipoMagnetico")
                a.text = "Tipo Magnético: " + managerNullables(elementInfoDetail.tipo_magnetico);

            if (a.name == "txtSuscepMagnetica")
                a.text = "Susceptibilidad Magnética: " + managerNullables(elementInfoDetail.susceptibilidad_magnetica);

            if (a.name == "txtVolumenMolar")
                a.text = "Volumen Molar: " + managerNullables(elementInfoDetail.volumen_molar);

            if (a.name == "txtRadioPoisson")
                a.text = "Radio de Poisson: " + managerNullables(elementInfoDetail.radio_poisson);

            if (a.name == "txtNumCuanticos")
                a.text = "Números Cuánticos: " + managerNullables(elementInfoDetail.numeros_cuanticos);

            if (a.name == "txtIndRefractivo")
                a.text = "Índice Refractivo: " + managerNullables(elementInfoDetail.indice_refractivo);

            if (a.name == "txtResistividad")
                a.text = "Resistividad: " + managerNullables(elementInfoDetail.resistividad);

            if (a.name == "txtCondTermica")
                a.text = "Condutividad Térmica: " + managerNullables(elementInfoDetail.conductividad_termica);

            if (a.name == "txtPuntoSupercond")
                a.text = "Punto de Superconductividad: " + managerNullables(elementInfoDetail.punto_superconductividad);

            if (a.name == "txtExpTermica")
                a.text = "Expansión Térmica: " + managerNullables(elementInfoDetail.expansion_termica);

            if (a.name == "txtVelSonido")
                a.text = "Velocidad del sonido en el elemento: " + managerNullables(elementInfoDetail.velocidad_sonido);

            if (a.name == "txtNroGrupoEsp")
                a.text = "Número de Grupos Espaciales: " + managerNullables(elementInfoDetail.numero_grupos_espaciales);

            if (a.name == "txtNombGrupoEsp")
                a.text = "Nombre del Grupo Espacial: " + managerNullables(elementInfoDetail.nombre_grupo_espacial);

            if (a.name == "txtRadioVanDerWaals")
                a.text = "Radio de Van Der Waals: " + managerNullables(elementInfoDetail.radio_van_der_waals);

            if (a.name == "txtRadioAtomAngs")
                a.text = "Radio atómico en Angstroms: " + managerNullables(elementInfoDetail.radio_atomico_en_angstroms);

            if (a.name == "txtRadioCovAngs")
                a.text = "Radio covalente (Å): " + managerNullables(elementInfoDetail.radio_covalente_en_angstroms);

            if (a.name == "txtRadioVanDerWaalsAngs")
                a.text = "Radio de Van der Waals (Å): " + managerNullables(elementInfoDetail.radio_van_der_waals_en_angstroms);

            if (a.name == "txtModuloYoung")
                a.text = "Módulo de Young: " + managerNullables(elementInfoDetail.modulo_young);

            if (a.name == "txtNombresAlotropicos")
                a.text = "Nombres alotrópicos: " + managerNullables(elementInfoDetail.nombres_alotropicos);

            if (a.name == "txtEnergiasIon")
                a.text = "Energías de Ionización: " + managerNullables(elementInfoDetail.energias_de_ionizacion);
        }
    }

    private string managerNullables(Nullable<float> valor)
    {
        if (!valor.HasValue)    
            return "n/a";
        return Convert.ToString(valor.Value);
    }

    private string managerNullables(String valor)
    {
        if (valor == null || valor == "" || valor == string.Empty)        
            return "n/a";
        return valor;
    }
    #endregion
}
