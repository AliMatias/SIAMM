using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QryTips : MonoBehaviour
{
    private DBManager dBManager = null;

    private void Awake()
    {
        dBManager = FindObjectOfType<DBManager>();
    }

    //trae un tip particular
    public TipsData GetTipById(int tipId)
    {
        TipsData tipData = null;
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT id, tema_relacionado, tema_tratado, descripcion " +
            "FROM  tips WHERE id=" + tipId + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string temaRelacionado = dBManager.SafeGetString(reader, 1);
                string temaTratado = dBManager.SafeGetString(reader, 2);
                string descripcion = dBManager.SafeGetString(reader, 3);

                tipData = new TipsData(id, temaRelacionado, temaTratado, descripcion);
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
        return tipData;
    }


    //trae el listado de id de tips para llenar los contadores de aparicion
    public List <int> GetTipsIds()
    {
        List<int> tipId = new List<int>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;
        try
        {
            string sqlQuery = "SELECT id FROM tips ORDER BY id;";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                tipId.Add(reader.GetInt32(0));
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
        return tipId;
    }

}
