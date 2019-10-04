using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QryMaterials : MonoBehaviour
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

    public List<MaterialMappingData> GetMaterialByAtomId(int atomId)
    {
        List<MaterialMappingData> result = new List<MaterialMappingData>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT * FROM materiales_mapping_element WHERE id_elemento=" + atomId;
            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);
            while (reader.Read())
            {
                int idMat = reader.GetInt32(0);
                int idElem = reader.GetInt32(1);
                int idMol = reader.GetInt32(2);
                int amount = reader.GetInt32(3);

                result.Add(new MaterialMappingData(idMat, idElem, idMol, amount));
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

    public MaterialData GetMaterialById(int materialId){
        MaterialData data = null;        
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;
        try{
            string sqlQuery = "SELECT * FROM materiales_lista WHERE id=" + materialId;
             //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);
            while (reader.Read())
            {
                int idMat = reader.GetInt32(0);
                string name = reader.GetString(1);
                string modelFile = reader.GetString(2);

                data = new MaterialData(idMat, name, modelFile);
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
        return data;
    }

    public List<MaterialMappingData> GetAllMaterialMappings()
    {
        List<MaterialMappingData> materials = new List<MaterialMappingData>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT * FROM materiales_mapping_element;";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int idMat = reader.GetInt32(0);
                int idEle = reader.GetInt32(1);
                int idMol = reader.GetInt32(2);
                int amount = reader.GetInt32(3);
                MaterialMappingData materialMapping = new MaterialMappingData(idMat, idEle, idMol, amount);
                materials.Add(materialMapping);
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

        return materials;
    }

    public List<MaterialData> GetAllMaterialsIn(List<int> materialIdList)
    {
        List<MaterialData> materials = new List<MaterialData>();
        if (materialIdList != null && materialIdList.Count <= 0) return materials;

        string idList = "(" + String.Join(",", materialIdList) + ")";

        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT * FROM materiales_lista WHERE id IN " + idList + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int idMat = reader.GetInt32(0);
                string name = reader.GetString(1);
                string modelFile = reader.GetString(2);
                MaterialData materialData = new MaterialData(idMat, name, modelFile);
                materials.Add(materialData);
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

        return materials;
    }

    #endregion
}
