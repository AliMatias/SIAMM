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
    
    public MaterialMappingData GetMaterialByMoleculeId(int moleculeId)
    {
        MaterialMappingData result = null;
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try{
            string sqlQuery = "SELECT id_material, id_elemento, id_molecula, cantidad FROM materiales_mapping_element WHERE id_molecula=" + moleculeId;
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
            string sqlQuery = "SELECT id_material, id_elemento, id_molecula, cantidad  FROM materiales_mapping_element WHERE id_elemento=" + atomId;
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

    public MaterialData GetMaterialById(int materialId)
    {
        MaterialData data = null;        
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;
        try
        {
            string sqlQuery = "SELECT  id,	nombre, archivo_modelo, clasificacion, caracteristicas, propiedades, usos, notas " +
            "FROM materiales_lista WHERE id=" + materialId + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);
            while (reader.Read())
            {
                int idMat = reader.GetInt32(0);
                string name = reader.GetString(1);
                string modelFile = reader.GetString(2);
                string clasificacion = dBManager.SafeGetString(reader, 3);
                string caracteristicas = dBManager.SafeGetString(reader, 4);
                string propiedades = dBManager.SafeGetString(reader, 5);
                string usos = dBManager.SafeGetString(reader, 6);
                string notas = dBManager.SafeGetString(reader, 7); 

                data = new MaterialData(idMat, name, modelFile, clasificacion, caracteristicas, propiedades, usos, notas);
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
    
    #endregion
}
