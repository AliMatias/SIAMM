using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

//servicio de conexión con la base de datos.
public class DBManager : MonoBehaviour
{
    private string connectionString;

    //al inciar, setea el path a la db
    void Start()
    {
        //path donde se encuentra la base de datos, "Application.dataPath" es el path x default en donde guarda unity
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
                string sqlQuery = "select Nombre, Protones, Neutrones, Electrones, Simbolo, Numero from ValidaElementos";
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
                string sqlQuery = "SELECT Nombre, Simbolo, Protones, Neutrones, Electrones FROM ValidaElementos WHERE Protones="
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

    //trae un elemento a partir del nombre
    public ElementData GetElementFromName(string name)
    {
        ElementData elementData = new ElementData();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT Nombre, Simbolo, Protones, Neutrones, Electrones FROM ValidaElementos WHERE Nombre='"
                    + name + "';";

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
