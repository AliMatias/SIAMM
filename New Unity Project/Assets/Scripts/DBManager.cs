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

    public String GetElementFromParticles(int protons, int neutrons, int electrons)
    {
        string element = null;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT name FROM element WHERE protons="
                    + protons + " AND neutrons=" + neutrons 
                    + " AND electrons=" + electrons + ";";
                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        element = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return element;
    }

    /*Get Elemento valida par proton neutron*/
    public String GetElementProtonNeutron(int protons, int neutrons)
    {
        string element = null;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT Nombre FROM ValidaElementos WHERE Protones="
                    + protons + " AND Neutrones=" + neutrons + ";";
               
                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        element = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return element;
    }


    /*Get Elemento valida par proton electron*/
    public String GetElementProtonElectron(int protons, int electron)
    {
        string element = null;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT Nombre FROM ValidaElementos WHERE Protones="
                    + protons + " AND Electrones=" + electron + ";";

                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        element = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return element;
    }
}
