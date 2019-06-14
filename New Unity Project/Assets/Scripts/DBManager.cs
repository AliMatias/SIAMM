using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

//servicio de conexión con la base de datos.
public class DBManager : MonoBehaviour
{
    private string connectionString;

    //al inciar, setea el path a la db
    void Start()
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
                string sqlQuery = "SELECT Nombre, Simbolo, Protones, Neutrones, Electrones FROM valida_elementos WHERE Protones="
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
                sqlQuery = sqlQuery + "CASE WHEN fase IS NULL THEN 'n/a' ELSE fase END,";
                sqlQuery = sqlQuery + "CASE WHEN estructura_cristalina IS NULL THEN 'n/a' ELSE estructura_cristalina END,";
                sqlQuery = sqlQuery + "CASE WHEN color IS NULL THEN 'n/a' ELSE color END,";
                sqlQuery = sqlQuery + "CASE WHEN valencia IS NULL THEN 'n/a' ELSE valencia END,";
                sqlQuery = sqlQuery + "CASE WHEN numeros_oxidacion IS NULL THEN 'n/a' ELSE numeros_oxidacion END,";
                sqlQuery = sqlQuery + "CASE WHEN configuracion_electronica IS NULL THEN 'n/a' ELSE configuracion_electronica END,";
                sqlQuery = sqlQuery + "caracteristicas,";
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
                        elementInfoBasic.Fase = reader.GetString(5);
                        elementInfoBasic.EstructuraCristalina= reader.GetString(6);
                        elementInfoBasic.Color = reader.GetString(7);
                        elementInfoBasic.Valencia = reader.GetString(8);
                        elementInfoBasic.NumerosOxidacion = reader.GetString(9);
                        elementInfoBasic.ConfElectronica = reader.GetString(10);
                        elementInfoBasic.Caracteristicas = reader.GetString(11);
                        elementInfoBasic.PuntoFusion = reader.GetString(12);
                        elementInfoBasic.PuntoEbullicion = reader.GetString(13);
                        elementInfoBasic.Resumen = reader.GetString(14);
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
                string sqlQuery = "SELECT nombre, simbolo, protones, neutrones, electrones FROM valida_elementos WHERE simbolo='"
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
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return elementData;
    }
}
