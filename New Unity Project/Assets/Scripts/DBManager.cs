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
        getElement();
    }
    
    private void getElement()
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
                        Debug.Log(reader.GetString(1));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }
}
