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
        //getAllElements();
    }
    
    private void getAllElements()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand command = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM element";
                command.CommandText = sqlQuery;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string message = "Element: " + reader.GetString(1) + ". Protons: " + reader.GetInt32(2) +
                            ". Neutrons: " + reader.GetInt32(3) + ". Electrons: " + reader.GetInt32(4) + ".";
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
}
