﻿using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//servicio de conexión con la base de datos.
public class DBManager : MonoBehaviour
{
    private string connectionString;

    //al inciar, setea el path a la db
    void Awake()
    {
        //path donde se encuentra la base de datos, "Application.dataPath" es el path x default en donde guarda unity
        //  //Data Source cannot be empty.  Use :memory: to open an in-memory database 
        connectionString = "URI=file:" + Application.dataPath + "/SIAMM.db";
        //valida HOY conexion ok con la base y trae tabla elementos
        getAllElements();
    }

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
    public ElementInfoBasic GetElementInfoBasica(string simbol)
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
                sqlQuery = sqlQuery + "WHERE simbolo='"
                + simbol + "';";

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
                        elementInfoBasic.EstructuraCristalina = reader.GetString(8);
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

    public MoleculeData GetMoleculeById(int moleculaId)
    {
        MoleculeData moleculeData = null;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional " +
                    "FROM moleculas_lista WHERE id=" + moleculaId + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string formula = reader.GetString(1);
                        string systematicNm = reader.GetString(2);
                        string stockNm = reader.GetString(3);
                        string traditionalNm = reader.GetString(4);
                        moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return moleculeData;
    }

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

    public List<MoleculeData> GetAllMolecules()
    {
        List<MoleculeData> molecules = new List<MoleculeData>();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional " +
                    "FROM moleculas_lista;";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string formula = reader.GetString(1);
                        string systematicNm = reader.GetString(2);
                        string stockNm = reader.GetString(3);
                        string traditionalNm = reader.GetString(4);
                        MoleculeData moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm);
                        molecules.Add(moleculeData);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return molecules;
    }
}
