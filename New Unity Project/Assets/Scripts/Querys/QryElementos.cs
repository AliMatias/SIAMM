using Mono.Data.Sqlite;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QryElementos : MonoBehaviour
{
    private DBManager dBManager = null;

    private void Awake()
    {
        dBManager = FindObjectOfType<DBManager>();
    }

    #region Metodos Exec Querys & Management
    //trae todos los elementos de la tabla
    public void getAllElements()
    {
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            //armo mi query
            string sqlQuery = "select Nombre, Protones, Neutrones, Electrones, Simbolo, Numero from valida_elementos";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            //mientras haya algo para leer, lo lee
            while (reader.Read())
            {
                //arma un mensaje extrayendo la data del propio reader. Cada índice equivale a la posición en el select
                string message = "Elemento: " + reader.GetString(0).ToString()
                    + ". Protones: " + reader.GetInt32(1).ToString()
                    + ". Neutrones: " + reader.GetInt32(2).ToString()
                    + ". Electrones: " + reader.GetInt32(3).ToString()
                    + ". Simbolo: " + reader.GetString(4).ToString()
                    + ". Numero Atomico: " + reader.GetInt32(5).ToString()
                    + ".";

                //esta salida queda por consola no hace falta popup
                Debug.Log(message);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }           
    }

    //trae un elemento a partir de los protones
    public ElementData GetElementFromProton(int protons)
    {
        ElementData elementData = new ElementData();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT Nombre, Simbolo, Protones, Neutrones, Electrones, Numero, maxelectronesgana, maxelectronespierde FROM valida_elementos WHERE Protones="
            + protons + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                elementData.Name = reader.GetString(0);
                elementData.Simbol = reader.GetString(1);
                elementData.Protons = reader.GetInt32(2);
                elementData.Neutrons = reader.GetInt32(3);
                elementData.Electrons = reader.GetInt32(4);
                elementData.Numero = reader.GetInt32(5);
                elementData.MaxElectronsGana = reader.GetInt32(6);
                elementData.MaxElectronsPierde = reader.GetInt32(7);
            }

        }
        catch (Exception e)
        {    
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return elementData;
    }

    //trae un elemento DE LA TABLA PERIODICA A PARTIR DEL NRO, para los BOTONES
    public ElementTabPer GetElementFromNro(int nro)
    {
        ElementTabPer elementTabPer = new ElementTabPer();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
           string sqlQuery = "SELECT simbolo, peso_atomico, configuracion_electronica_abreviada, " +
           "nombre, clasificacion_grupo " +
           "FROM elementos_info_basica WHERE numero_atomico="
           + nro + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                elementTabPer.Nroatomico = nro;
                elementTabPer.Simbol = reader.GetString(0);
                elementTabPer.PesoAtomico = reader.GetFloat(1);
                elementTabPer.ConfElectronica = dBManager.SafeGetString(reader, 2);
                elementTabPer.Name = reader.GetString(3);
                elementTabPer.ClasificacionGrupo = reader.GetString(4);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return elementTabPer;
    }

    //trae un elemento a partir del símbolo
    public ElementData GetElementFromName(string simbol)
    {
        ElementData elementData = new ElementData();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT nombre, simbolo, protones, neutrones, electrones, numero FROM valida_elementos WHERE simbolo='"
            + simbol + "';";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                elementData.Name = reader.GetString(0);
                elementData.Simbol = reader.GetString(1);
                elementData.Protons = reader.GetInt32(2);
                elementData.Neutrons = reader.GetInt32(3);
                elementData.Electrons = reader.GetInt32(4);
                elementData.Numero = reader.GetInt32(5);
            }
        }
        catch (Exception e)
        {        
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return elementData;
    }

    //trae un elemento isotopo a partir del nro atomico del elemento
    public IsotopoData GetIsotopo(int neutrones, int numeroAtomico)
    {
        IsotopoData isotopoData = new IsotopoData();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT id, isotopo, numero_de_masa, estable FROM valida_isotopos WHERE neutrones="
            + neutrones + " AND numero_atomico=" + numeroAtomico + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                isotopoData.Id = reader.GetInt32(0);
                isotopoData.Name = reader.GetString(1);
                isotopoData.Masa = reader.GetInt32(2);
                isotopoData.Estable = reader.GetInt32(3);
            }           
        }
        catch (Exception e)
        { 
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }
        return isotopoData;
    }



    //trae TODOS los datos del isotopo a partir del nro atomico del elemento
    public IsotopoAllData GetAllDataIsotopo(int idIsotopo)
    {
        IsotopoAllData isotopoData = new IsotopoAllData();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT id, numero_atomico, numero_correlativo, isotopo, numero_masa, masa_atomica_relativa, composicion_isotopica, peso_atomico_estandar FROM isotopos WHERE id="
            + idIsotopo + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                isotopoData.Id = reader.GetInt32(0);
                isotopoData.NumeroAtomico = reader.GetInt32(1);
                isotopoData.NumeroCorrelativo = reader.GetInt32(2);
                isotopoData.Isotopo = dBManager.SafeGetString(reader, 3);
                isotopoData.NumeroMasa = reader.GetInt32(4);
                isotopoData.MasaAtomicaRelativa = dBManager.SafeGetString(reader, 5);
                isotopoData.ComposicionIsotopica = dBManager.SafeGetString(reader, 6);
                isotopoData.PesoAtomicoEstandar = dBManager.SafeGetString(reader, 7);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }
        return isotopoData;
    }


    //trae de acuerdo a la orbita la cantidad maxima de electrones
    public OrbitData GetOrbitDataByNumber(int orbitNumber)
    {
        OrbitData orbitData = null;
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT nro_orbita, nombre_capa, max_electrones FROM elementos_orbitas WHERE nro_orbita ="
            + orbitNumber + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int number = reader.GetInt32(0);
                string name = reader.GetString(1);
                int maxElectrons = reader.GetInt32(2);
                orbitData = new OrbitData(number, name, maxElectrons);
            }
                 
        }
        catch (Exception e)
        {       
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return orbitData;
    }


    //trae la informacion basica de un elemento de la tabla periodica a partir de su SIMBOLO
    public ElementInfoBasic GetElementInfoBasica(int nroAtomico)
    {
        ElementInfoBasic elementInfoBasic = new ElementInfoBasic();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection  =  null;
        
        try
        {
            string sqlQuery = "SELECT numero_atomico, simbolo, nombre, peso_atomico, periodo,";
            sqlQuery = sqlQuery + "clasificacion, clasificacion_grupo,";
            sqlQuery = sqlQuery + "estado_natural,";
            sqlQuery = sqlQuery + "estructura_cristalina,";
            sqlQuery = sqlQuery + "color,";
            sqlQuery = sqlQuery + "valencia,";
            sqlQuery = sqlQuery + "numeros_oxidacion,";
            sqlQuery = sqlQuery + "configuracion_electronica,";
            sqlQuery = sqlQuery + "punto_fusion,";
            sqlQuery = sqlQuery + "punto_ebullicion,";
            sqlQuery = sqlQuery + "resumen ";
            sqlQuery = sqlQuery + "FROM elementos_info_basica ";
            sqlQuery = sqlQuery + "WHERE numero_atomico='"
            + nroAtomico + "';";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                elementInfoBasic.Nroatomico = reader.GetInt32(0);
                elementInfoBasic.Simbol = reader.GetString(1);
                elementInfoBasic.Name = reader.GetString(2);
                elementInfoBasic.PesoAtomico = reader.GetFloat(3);
                elementInfoBasic.Periodo = reader.GetInt32(4);
                elementInfoBasic.Clasificacion = reader.GetString(5);
                elementInfoBasic.Clasificacion_grupo = reader.GetString(6);
                elementInfoBasic.Estado_natural = dBManager.SafeGetString(reader, 7);
                elementInfoBasic.EstructuraCristalina = dBManager.SafeGetString(reader, 8);
                elementInfoBasic.Color = dBManager.SafeGetString(reader, 9);
                elementInfoBasic.Valencia = dBManager.SafeGetString(reader, 10);
                elementInfoBasic.NumerosOxidacion = dBManager.SafeGetString(reader, 11);
                elementInfoBasic.ConfElectronica = dBManager.SafeGetString(reader, 12);
                elementInfoBasic.PuntoFusion = dBManager.SafeGetString(reader, 13);
                elementInfoBasic.PuntoEbullicion = dBManager.SafeGetString(reader, 14);
                elementInfoBasic.Resumen = reader.GetString(15);
            }
        }
        catch (Exception e)
        {         
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return elementInfoBasic;
    }

    //trae colores de elementos
    public Color32 GetElementColor(int elementId){
        Color32 result = new Color32(0,0,0,1);
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try{
            string sqlQuery = "SELECT * FROM elementos_colores WHERE id_elemento=" + elementId + ";";

            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);
            while(reader.Read()){
                result = new Color32((byte)reader.GetInt32(1), (byte)reader.GetInt32(2), (byte)reader.GetInt32(3), 1);
            }
        }
        catch (Exception e)
        {         
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }
        return result;
    }

    //trae sugerencias de elementos
    public List<Suggestion> GetSuggestionForElement(int elementId){
        List<Suggestion> suggestions = new List<Suggestion>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try{
            string sqlQuery = "SELECT * FROM sugerencias WHERE id_elemento=" + elementId + ";";

            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);
            while(reader.Read()){
                suggestions.Add(new Suggestion(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
            }
        }
        catch (Exception e)
        {         
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }
        return suggestions;
    }

    //trae la info detallada a partir de un nro atomico entero
    public ElementInfoDetail GetElementInfoDetail(int nroAtomico)
    {
        ElementInfoDetail elementInfoDetail = new ElementInfoDetail();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            //tener en cuenta los null sino tirara error de cast luego en el read del set    
            string sqlQuery = "SELECT numero_atomico, isotopos_estables, isotopos_aplicaciones, tipo_electrico, radiactivo, abundancia_corteza_terrestre, descubrimiento,";
            sqlQuery = sqlQuery + "descubierto_por, angulos_de_red, vida_media, modulo_compresibilidad, dureza_brinell, presion_critica, temperatura_critica, conductividad_electrica,";
            sqlQuery = sqlQuery + "densidad, radio_covalente, afinidad_electronica, punto_curie, modo_decaimiento, electronegatividad, densidadliquida, constante_red,";
            sqlQuery = sqlQuery + "multiplicidad_atomica_gas, calor_de_fusion, calor_de_vaporizacion, tipo_magnetico, susceptibilidad_magnetica, volumen_molar,";
            sqlQuery = sqlQuery + "radio_poisson, numeros_cuanticos, indice_refractivo, resistividad, conductividad_termica, punto_superconductividad, expansion_termica,";
            sqlQuery = sqlQuery + "velocidad_sonido, numero_grupos_espaciales, nombre_grupo_espacial, radio_van_der_waals, radio_atomico_en_angstroms,";
            sqlQuery = sqlQuery + "radio_covalente_en_angstroms, radio_van_der_waals_en_angstroms, modulo_young, nombres_alotropicos, energias_de_ionizacion ";
            sqlQuery = sqlQuery + "FROM elementos_info_detalle ";
            sqlQuery = sqlQuery + "WHERE numero_atomico="
            + nroAtomico + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            //por cada atributo de la clase info detallada hay que validar los nulls
            while (reader.Read())
            {
                elementInfoDetail = new ElementInfoDetail(nroAtomico, dBManager.SafeGetString(reader, 1),
                   dBManager.SafeGetString(reader, 2), dBManager.SafeGetString(reader, 3), dBManager.SafeGetString(reader, 4), dBManager.SafeGetString(reader, 5),
                   dBManager.SafeGetString(reader, 6), dBManager.SafeGetString(reader, 7), dBManager.SafeGetString(reader, 8), dBManager.SafeGetString(reader, 9),
                   dBManager.SafeGetString(reader, 10), dBManager.SafeGetString(reader, 11), dBManager.SafeGetString(reader, 12), dBManager.SafeGetString(reader, 13),
                   dBManager.SafeGetString(reader, 14), dBManager.SafeGetFloat(reader, 15), dBManager.SafeGetString(reader, 16), dBManager.SafeGetString(reader, 17),
                   dBManager.SafeGetString(reader, 18), dBManager.SafeGetString(reader, 19), dBManager.SafeGetFloat(reader, 20), dBManager.SafeGetString(reader, 21),
                   dBManager.SafeGetString(reader, 22), dBManager.SafeGetString(reader, 23), dBManager.SafeGetString(reader, 24), dBManager.SafeGetString(reader, 25),
                   dBManager.SafeGetString(reader, 26), dBManager.SafeGetString(reader, 27), dBManager.SafeGetFloat(reader, 28), dBManager.SafeGetString(reader, 29),
                   dBManager.SafeGetString(reader, 30), dBManager.SafeGetFloat(reader, 31), dBManager.SafeGetString(reader, 32), dBManager.SafeGetString(reader, 33),
                   dBManager.SafeGetString(reader, 34), dBManager.SafeGetString(reader, 35), dBManager.SafeGetString(reader, 36), dBManager.SafeGetFloat(reader, 37),
                   dBManager.SafeGetString(reader, 38), dBManager.SafeGetString(reader, 39), dBManager.SafeGetFloat(reader, 40), dBManager.SafeGetFloat(reader, 41),
                   dBManager.SafeGetFloat(reader, 42), dBManager.SafeGetString(reader, 43), dBManager.SafeGetString(reader, 44), dBManager.SafeGetString(reader, 45));
            }
        }
        catch (Exception e)
        {         
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return elementInfoDetail;
    }


    //trae la informacion de 6 campos seleccionados para mostrar en el panel inferior
    public ElementInfoPanelInfo GetElementInfoPanelSuggestion(int nroAtomico)
    {
        ElementInfoPanelInfo elementInfoPanelInfo = new ElementInfoPanelInfo();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            //tener en cuenta los null sino tirara error de cast luego en el read del set    
            string sqlQuery = "SELECT clasificacion, clasificacion_grupo, numeros_oxidacion, punto_fusion, punto_ebullicion, distribucion_de_electrones_por_niveles ";
            sqlQuery = sqlQuery + "FROM elementos_info_basica ";
            sqlQuery = sqlQuery + "  INNER JOIN elementos_info_detalle ";
            sqlQuery = sqlQuery + "     ON elementos_info_basica.numero_atomico = elementos_info_detalle.numero_atomico ";
            sqlQuery = sqlQuery + "WHERE elementos_info_basica.numero_atomico="
            + nroAtomico + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            //por cada atributo de la clase info detallada hay que validar los nulls
            while (reader.Read())
            {
                elementInfoPanelInfo = new ElementInfoPanelInfo(dBManager.SafeGetString(reader, 0), dBManager.SafeGetString(reader, 1),
                   dBManager.SafeGetString(reader, 2), dBManager.SafeGetString(reader, 3), dBManager.SafeGetString(reader, 4), dBManager.SafeGetString(reader, 5));
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            dBManager.ManageClosing(dbConnection, reader);
        }

        return elementInfoPanelInfo;
    }

    #endregion
}
