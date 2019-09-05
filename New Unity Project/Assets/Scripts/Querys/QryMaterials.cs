using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QryMoleculas : MonoBehaviour
{
    private DBManager dBManager = null;

    private void Awake()
    {
        dBManager = FindObjectOfType<DBManager>();
    }

    #region Metodos Exec Querys & Management
    
    public MaterialMappingData GetMaterialByMoleculeId(int moleculeId){
        MaterialMappingData result = null;

        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try{
            string sqlQuery = "SELECT * FROM materiales_mapping_element WHERE id_molecula=" + moleculeId;

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);
            while (reader.Read())
            {
                int idMat = reader.GetInt32(0);
                int idElem = reader.GetInt32(1);
                int idMol = reader.GetInt32(2);
                int amount = reader.GetInt32(3);

                result = new MaterialMappingData(idMat, idElem, idMol, amount);
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
    
    #endregion
}
