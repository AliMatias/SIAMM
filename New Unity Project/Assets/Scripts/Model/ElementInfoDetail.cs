using System;

//DTO de info detallada
public class ElementInfoDetail
{
    public int nroatomico { get; set; }
    public string isotopos_estables { get; set; }
    public string isotopos_aplicaciones { get; set; }
    public string tipo_electrico { get; set; }
    public string radiactivo { get; set; }
    public string abundancia_corteza_terrestre { get; set; }
    public string descubrimiento { get; set; }
    public string descubierto_por { get; set; }
    public string angulos_de_red { get; set; }
    public string vida_media { get; set; }
    public string modulo_compresibilidad { get; set; }
    public string dureza_brinell { get; set; }
    public string presion_critica { get; set; }
    public string temperatura_critica { get; set; }
    public string conductividad_electrica { get; set; } 
    public Nullable<float> densidad { get; set; }
    public string radio_covalente { get; set; }
    public string afinidad_electronica { get; set; }
    public string punto_curie { get; set; }
    public string modo_decaimiento { get; set; } 
    public Nullable<float> electronegatividad { get; set; }
    public string densidadliquida { get; set; }
    public string constante_red { get; set; }
    public string multiplicidad_atomica_gas { get; set; }
    public string calor_de_fusion { get; set; }
    public string calor_de_vaporizacion { get; set; }
    public string tipo_magnetico { get; set; }
    public string susceptibilidad_magnetica { get; set; } 
    public Nullable<float> volumen_molar { get; set; }
    public string radio_poisson { get; set; }
    public string numeros_cuanticos { get; set; } 
    public Nullable<float> indice_refractivo { get; set; }
    public string resistividad { get; set; }
    public string conductividad_termica { get; set; }
    public string punto_superconductividad { get; set; }
    public string expansion_termica { get; set; }
    public string velocidad_sonido { get; set; } 
    public Nullable<float> numero_grupos_espaciales { get; set; }
    public string nombre_grupo_espacial { get; set; }
    public string radio_van_der_waals { get; set; } 
    public Nullable<float> radio_atomico_en_angstroms { get; set; }
    public Nullable<float> radio_covalente_en_angstroms { get; set; }
    public Nullable<float> radio_van_der_waals_en_angstroms { get; set; }
    public string modulo_young { get; set; }
    public string nombres_alotropicos { get; set; }
    public string energias_de_ionizacion { get; set; }

    //constructor
    public ElementInfoDetail()
    {
    }

    public ElementInfoDetail(int nroatomico, string isotopos_estables, string isotopos_aplicaciones, string tipo_electrico, string radiactivo, string abundancia_corteza_terrestre, string descubrimiento, string descubierto_por, string angulos_de_red, string vida_media, string modulo_compresibilidad, string dureza_brinell, string presion_critica, string temperatura_critica, string conductividad_electrica, Nullable<float> densidad, string radio_covalente, string afinidad_electronica, string punto_curie, string modo_decaimiento, Nullable<float> electronegatividad, string densidadliquida, string constante_red, string multiplicidad_atomica_gas, string calor_de_fusion, string calor_de_vaporizacion, string tipo_magnetico, string susceptibilidad_magnetica, Nullable<float> volumen_molar, string radio_poisson, string numeros_cuanticos, Nullable<float> indice_refractivo, string resistividad, string conductividad_termica, string punto_superconductividad, string expansion_termica, string velocidad_sonido, Nullable<float> numero_grupos_espaciales, string nombre_grupo_espacial, string radio_van_der_waals, Nullable<float> radio_atomico_en_angstroms, Nullable<float> radio_covalente_en_angstroms, Nullable<float> radio_van_der_waals_en_angstroms, string modulo_young, string nombres_alotropicos, string energias_de_ionizacion)
    {
        this.nroatomico = nroatomico;
        this.isotopos_estables = isotopos_estables;
        this.isotopos_aplicaciones = isotopos_aplicaciones;
        this.tipo_electrico = tipo_electrico;
        this.radiactivo = radiactivo;
        this.abundancia_corteza_terrestre = abundancia_corteza_terrestre;
        this.descubrimiento = descubrimiento;
        this.descubierto_por = descubierto_por;
        this.angulos_de_red = angulos_de_red;
        this.vida_media = vida_media;
        this.modulo_compresibilidad = modulo_compresibilidad;
        this.dureza_brinell = dureza_brinell;
        this.presion_critica = presion_critica;
        this.temperatura_critica = temperatura_critica;
        this.conductividad_electrica = conductividad_electrica;
        this.densidad = densidad;
        this.radio_covalente = radio_covalente;
        this.afinidad_electronica = afinidad_electronica;
        this.punto_curie = punto_curie;
        this.modo_decaimiento = modo_decaimiento;
        this.electronegatividad = electronegatividad;
        this.densidadliquida = densidadliquida;
        this.constante_red = constante_red;
        this.multiplicidad_atomica_gas = multiplicidad_atomica_gas;
        this.calor_de_fusion = calor_de_fusion;
        this.calor_de_vaporizacion = calor_de_vaporizacion;
        this.tipo_magnetico = tipo_magnetico;
        this.susceptibilidad_magnetica = susceptibilidad_magnetica;
        this.volumen_molar = volumen_molar;
        this.radio_poisson = radio_poisson;
        this.numeros_cuanticos = numeros_cuanticos;
        this.indice_refractivo = indice_refractivo;
        this.resistividad = resistividad;
        this.conductividad_termica = conductividad_termica;
        this.punto_superconductividad = punto_superconductividad;
        this.expansion_termica = expansion_termica;
        this.velocidad_sonido = velocidad_sonido;
        this.numero_grupos_espaciales = numero_grupos_espaciales;
        this.nombre_grupo_espacial = nombre_grupo_espacial;
        this.radio_van_der_waals = radio_van_der_waals;
        this.radio_atomico_en_angstroms = radio_atomico_en_angstroms;
        this.radio_covalente_en_angstroms = radio_covalente_en_angstroms;
        this.radio_van_der_waals_en_angstroms = radio_van_der_waals_en_angstroms;
        this.modulo_young = modulo_young;
        this.nombres_alotropicos = nombres_alotropicos;
        this.energias_de_ionizacion = energias_de_ionizacion;
    }
}