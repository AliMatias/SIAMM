using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Mono.Data.Sqlite;

public class DBManager : MonoBehaviour
{
    private string connectionString;

    // Start is called before the first frame update
    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/SIAMM.db";
        getAllElements();//valida HOY conexion ok con la base y trae tabla elementos
    }
    
    private void getAllElements()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                //string sqlQuery = "SELECT * FROM element";
                string sqlQuery = "select Nombre, Protones, Neutrones, Electrones, Simbolo, Numero from ValidaElementos";
                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // string message = "Element: " + reader.GetString(1) + ". Protons: " + reader.GetInt32(2) +
                        //    ". Neutrons: " + reader.GetInt32(3) + ". Electrons: " + reader.GetInt32(4) + ".";

                        string message = "Elemento: " + reader.GetString(0).ToString()                          
                            + ". Protones: " + reader.GetInt32(1).ToString() 
                            + ". Neutrones: " + reader.GetInt32(2).ToString() 
                            + ". Electrones: " + reader.GetInt32(3).ToString() 
                            + ". Simbolo: " + reader.GetString(4).ToString() 
                            + ". Numero Atomico: " + reader.GetInt32(5).ToString() 
                            + ".";

                        Debug.Log(message);
                    }
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
}
