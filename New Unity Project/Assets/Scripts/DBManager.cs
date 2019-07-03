using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;

//servicio de conexión con la base de datos.
public class DBManager : MonoBehaviour
{
    private string connectionString;
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;

    //al inciar, setea el path a la db
    void Start()
    {
        //path donde se encuentra la base de datos, "Application.dataPath" es el path x default en donde guarda unity
        //  //Data Source cannot be empty.  Use :memory: to open an in-memory database 
        connectionString = "URI=file:" + Application.dataPath + "/SIAMM.db";
        //valida HOY conexion ok con la base y trae tabla elementos
        getAllElements();
    }

    #region Metodos DB

    //trae todos los elementos de la tabla
    private void getAllElements()
    {
        //usa interfaz de conexión para crear una conexión
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            //abre conexión
            dbConnection.Open();
            //usa interfaz de comandos para crear un comando
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                //armo mi query
                string sqlQuery = "select Nombre, Protones, Neutrones, Electrones, Simbolo, Numero from valida_elementos";
                //le digo que es en formato texto
                command.CommandText = sqlQuery;
                //uso la interfaz de reader para ejecutar mi comando
                using (IDataReader reader = command.ExecuteReader())
                {
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

                        Debug.Log(message);
                    }
                    //cierro conexión y reader, HACER SIEMPRE
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    //trae un elemento a partir de los protones
    public ElementData GetElementFromProton(int protons)
    {
        ElementData elementData = new ElementData();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT Nombre, Simbolo, Protones, Neutrones, Electrones, Numero FROM valida_elementos WHERE Protones="
                    + protons + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elementData.Name = reader.GetString(0);
                        elementData.Simbol = reader.GetString(1);
                        elementData.Protons = reader.GetInt32(2);
                        elementData.Neutrons = reader.GetInt32(3);
                        elementData.Electrons = reader.GetInt32(4);
                        elementData.Numero = reader.GetInt32(5);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return elementData;
    }

    //trae un elemento DE LA TABLA PERIODICA A PARTIR DEL NRO, para los BOTONES
    public ElementTabPer GetElementFromNro(int nro)
    {
        ElementTabPer elementTabPer = new ElementTabPer();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand command = dbConnection.CreateCommand())
            {
                //tener en cuenta los null sino tirara error de cast luego en el read del set
                string sqlQuery = "SELECT simbolo, peso_atomico, CASE WHEN configuracion_electronica_abreviada IS NULL THEN 'n/a' ELSE configuracion_electronica_abreviada END, nombre FROM elementos_info_basica WHERE numero_atomico="
                    + nro + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elementTabPer.Nroatomico = nro;
                        elementTabPer.Simbol = reader.GetString(0);
                        elementTabPer.PesoAtomico = reader.GetFloat(1);
                        elementTabPer.ConfElectronica = reader.GetString(2);
                        elementTabPer.Name = reader.GetString(3);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
         return elementTabPer;
    }

    //trae la informacion basica de un elemento de la tabla periodica a partir de su SIMBOLO
    public ElementInfoBasic GetElementInfoBasica(int nroAtomico)
    {
        ElementInfoBasic elementInfoBasic = new ElementInfoBasic();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand command = dbConnection.CreateCommand())
            {
                //tener en cuenta los null sino tirara error de cast luego en el read del set
                string sqlQuery = "SELECT numero_atomico, simbolo, nombre, peso_atomico, periodo,";
                sqlQuery = sqlQuery + "CASE WHEN estado_natural IS NULL THEN 'n/a' ELSE estado_natural END,";
                sqlQuery = sqlQuery + "clasificacion, clasificacion_grupo,";
                sqlQuery = sqlQuery + "CASE WHEN estructura_cristalina IS NULL THEN 'n/a' ELSE estructura_cristalina END,";
                sqlQuery = sqlQuery + "CASE WHEN color IS NULL THEN 'n/a' ELSE color END,";
                sqlQuery = sqlQuery + "CASE WHEN valencia IS NULL THEN 'n/a' ELSE valencia END,";
                sqlQuery = sqlQuery + "CASE WHEN numeros_oxidacion IS NULL THEN 'n/a' ELSE numeros_oxidacion END,";
                sqlQuery = sqlQuery + "CASE WHEN configuracion_electronica IS NULL THEN 'n/a' ELSE configuracion_electronica END,";
                sqlQuery = sqlQuery + "CASE WHEN punto_fusion IS NULL THEN 'n/a' ELSE punto_fusion END,";
                sqlQuery = sqlQuery + "CASE WHEN punto_ebullicion IS NULL THEN 'n/a' ELSE punto_ebullicion END,";
                sqlQuery = sqlQuery + "resumen ";
                sqlQuery = sqlQuery + "FROM elementos_info_basica ";
                sqlQuery = sqlQuery + "WHERE numero_atomico='"
                + nroAtomico + "';";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elementInfoBasic.Nroatomico = reader.GetInt32(0);
                        elementInfoBasic.Simbol = reader.GetString(1);
                        elementInfoBasic.Name = reader.GetString(2);
                        elementInfoBasic.PesoAtomico = reader.GetFloat(3);
                        elementInfoBasic.Periodo = reader.GetInt32(4);
                        elementInfoBasic.Clasificacion = reader.GetString(5);
                        elementInfoBasic.Clasificacion_grupo = reader.GetString(6);
                        elementInfoBasic.Estado_natural = reader.GetString(7);
                        elementInfoBasic.EstructuraCristalina= reader.GetString(8);
                        elementInfoBasic.Color = reader.GetString(9);
                        elementInfoBasic.Valencia = reader.GetString(10);
                        elementInfoBasic.NumerosOxidacion = reader.GetString(11);
                        elementInfoBasic.ConfElectronica = reader.GetString(12);
                        elementInfoBasic.PuntoFusion = reader.GetString(13);
                        elementInfoBasic.PuntoEbullicion = reader.GetString(14);
                        elementInfoBasic.Resumen = reader.GetString(15);
                    }
                
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return elementInfoBasic;
    }

    //trae un elemento a partir del símbolo
    public ElementData GetElementFromName(string simbol)
    {
        ElementData elementData = new ElementData();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT nombre, simbolo, protones, neutrones, electrones, numero FROM valida_elementos WHERE simbolo='"
                    + simbol + "';";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elementData.Name = reader.GetString(0);
                        elementData.Simbol = reader.GetString(1);
                        elementData.Protons = reader.GetInt32(2);
                        elementData.Neutrons = reader.GetInt32(3);
                        elementData.Electrons = reader.GetInt32(4);
                        elementData.Numero = reader.GetInt32(5);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return elementData;
    }

    //trae un elemento a partir de los protones
    public ElementData GetIsotopo(int neutrones, int numeroAtomico)
    {
        ElementData elementData = new ElementData();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT isotopo FROM valida_isotopos WHERE neutrones="
                    + neutrones + " AND numero_atomico=" + numeroAtomico + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elementData.Name = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return elementData;
    }

    //trae de acuerdo a la orbita la cantidad maxima de electrones
    public OrbitData GetOrbitDataByNumber(int orbitNumber)
    {
        OrbitData orbitData = null;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT nro_orbita, nombre_capa, max_electrones FROM elementos_orbitas WHERE nro_orbita ="
                    + orbitNumber + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int number = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int maxElectrons = reader.GetInt32(2);
                        orbitData = new OrbitData(number, name, maxElectrons);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return orbitData;
    }

    //
    public List<int> GetMoleculesByAtomNumberAndQuantity(int atomNumber, int quantity)
    {
        List<int> posiblesMoleculas = new List<int>();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT id_molecula FROM moleculas_mapping_element WHERE id_elemento="
                    + atomNumber + " AND cantidad=" + quantity + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        posiblesMoleculas.Add(reader.GetInt32(0));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return posiblesMoleculas;
    }

    //trae los datos de una molecula en particular
    public MoleculeData GetMoleculeById(int moleculaId)
    {
        MoleculeData moleculeData = null;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional " + 
                    "FROM moleculas_lista WHERE id="+ moleculaId + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string formula = reader.GetString(0);
                        string systematicNm = reader.GetString(1);
                        string stockNm = reader.GetString(2);
                        string traditionalNm = reader.GetString(3);
                        moleculeData = new MoleculeData(formula, systematicNm, stockNm, traditionalNm);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return moleculeData;
    }

    //
    public int GetUniqueElementCountInMoleculeById(int moleculaId)
    {
        int elementCount = 0;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT count(1) FROM moleculas_mapping_element " +
                    "WHERE id_molecula=" + moleculaId +
                    " GROUP BY id_molecula;";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elementCount = reader.GetInt32(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return elementCount;
    }

    //trae la info detallada a partir de un nro atomico entero
    public ElementInfoDetail GetElementInfoDetail(int nroAtomico)
    {
        ElementInfoDetail elementInfoDetail = null;
        SqliteDataReader reader = null;

        try
        {

            this.dbConnection = openCon(this.connectionString);

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

            this.dbCommand = getCMD(sqlQuery);

            reader = EjecutaConsultaSql(this.dbConnection, this.dbCommand);

            while (reader.Read())
            {
                //elementInfoDetail = new (reader.GetInt32(0),
                //elementInfoBasic.Simbol = reader.GetString(1);
                //elementInfoBasic.Name = reader.GetString(2);
                //elementInfoBasic.PesoAtomico = reader.GetFloat(3);
                //elementInfoBasic.Periodo = reader.GetInt32(4);
                //elementInfoBasic.Clasificacion = reader.GetString(5);
                //elementInfoBasic.Clasificacion_grupo = reader.GetString(6);
                //elementInfoBasic.Estado_natural = reader.GetString(7);
                //elementInfoBasic.EstructuraCristalina = reader.GetString(8);
                //elementInfoBasic.Color = reader.GetString(9);
                //elementInfoBasic.Valencia = reader.GetString(10);
                //elementInfoBasic.NumerosOxidacion = reader.GetString(11);
                //elementInfoBasic.ConfElectronica = reader.GetString(12);
                //elementInfoBasic.PuntoFusion = reader.GetString(13);
                //elementInfoBasic.PuntoEbullicion = reader.GetString(14);
                //elementInfoBasic.Resumen = reader.GetString(15);

            }

        }
        catch (Exception e)
        {
            throw e;
        }

        finally
        {        
            if (reader != null)
                reader.Close();
            this.dbConnection.Close();
        }

        return elementInfoDetail;
    }



    //METODO PRINCIPAL GENERAL DE APERTURA DE CONECCIONES
    public SqliteConnection openCon(String connectionString)
    {
        SqliteConnection con = new SqliteConnection();
        try
        {
            con = GetCnxDB(connectionString);
            con.Open();
        }
        catch (Exception e)
        {
            throw e;
        }
        return con;
    }

    #endregion


    #region SqlLiteDbConnection

    public SqliteConnection GetCnxDB(String datoDb)
    {
        SqliteConnection cn = new SqliteConnection();
        try
        {
            cn.ConnectionString = datoDb;
        }
        catch (Exception e)
        {
            throw e;
        }
        return cn;
    }


    #endregion

    #region SqlLiteDbCommands
    public SqliteCommand getCMD(String script)
    {

        SqliteCommand aCommand = new SqliteCommand();
        aCommand.CommandText = script;
        aCommand.CommandType = CommandType.Text;

        return aCommand;
    }
    #endregion

    #region SqlConnectionCMDExec
    /*
     * Execute SQL query
     */
    public SqliteDataReader EjecutaConsultaSql(SqliteConnection cnparam, SqliteCommand cmd)
    {
        try
        {
            cmd.Connection = cnparam;
        }
        catch (Exception e)
        {
            throw e;
        }
        return cmd.ExecuteReader();
    }
    #endregion

}
