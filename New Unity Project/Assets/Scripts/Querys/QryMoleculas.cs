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

    //
    public List<int> GetMoleculesByAtomNumberAndQuantity(int atomNumber, int quantity)
    {
        List<int> posiblesMoleculas = new List<int>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT id_molecula FROM moleculas_mapping_element WHERE id_elemento="
            + atomNumber + " AND cantidad=" + quantity + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                posiblesMoleculas.Add(reader.GetInt32(0));
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

        return posiblesMoleculas;
    }


    //trae los datos de una molecula en particular
    public MoleculeData GetMoleculeById(int moleculaId)
    {
        MoleculeData moleculeData = null;
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional, caracteristicas, propiedades, usos, clasificacion, diferencia_electronegatividad " +
            "FROM moleculas_lista WHERE id=" + moleculaId + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string formula = reader.GetString(1);
                string systematicNm = reader.GetString(2);
                string stockNm = reader.GetString(3);
                string traditionalNm = reader.GetString(4);
                string caracteristicas = dBManager.SafeGetString(reader, 5);
                string propiedades = dBManager.SafeGetString(reader, 6);
                string usos = dBManager.SafeGetString(reader, 7);
                string clasificacion = dBManager.SafeGetString(reader, 8);
                Nullable<float> dif_electronegatividad = dBManager.SafeGetFloat(reader, 9);

                moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm, caracteristicas, propiedades, usos, clasificacion, dif_electronegatividad);
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
        return moleculeData;
    }


    //
    public int GetUniqueElementCountInMoleculeById(int moleculaId)
    {
        int elementCount = 0;
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT count(1) FROM moleculas_mapping_element " +
            "WHERE id_molecula=" + moleculaId +
            " GROUP BY id_molecula;";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                elementCount = reader.GetInt32(0);
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

        return elementCount;
    }


    //
    public List<AtomInMolPositionData> GetElementPositions(int inputMoleculeId)
    {
        List<AtomInMolPositionData> atomPositions = new List<AtomInMolPositionData>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT * from moleculas_posicion3D_element where id_molecula=" + inputMoleculeId;

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int moleculeId = reader.GetInt32(1);
                int elementId = reader.GetInt32(2);
                float posX = reader.GetFloat(3);
                float posY = reader.GetFloat(4);
                float posZ = reader.GetFloat(5);
                float scale = reader.GetFloat(6);
                int connectedTo = reader.GetInt32(7);
                int connectionType = reader.GetInt32(8);
                int lineType = reader.GetInt32(9);
                atomPositions.Add(new AtomInMolPositionData(id, moleculeId, elementId, posX,
                    posY, posZ, scale, connectedTo, connectionType, lineType));
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

        return atomPositions;
    }


    public List<MoleculeData> GetAllMolecules()
    {
        List<MoleculeData> molecules = new List<MoleculeData>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        { 
            string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional, " + 
                "caracteristicas, propiedades, usos, clasificacion, diferencia_electronegatividad FROM moleculas_lista;";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string formula = reader.GetString(1);
                string systematicNm = reader.GetString(2);
                string stockNm = reader.GetString(3);
                string traditionalNm = reader.GetString(4);
                string caracteristicas = dBManager.SafeGetString(reader, 5);
                string propiedades = dBManager.SafeGetString(reader, 6);
                string usos = dBManager.SafeGetString(reader, 7);
                string clasificacion = dBManager.SafeGetString(reader, 8);
                Nullable<float> dif_electronegatividad = dBManager.SafeGetFloat(reader, 9);
                MoleculeData moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm, caracteristicas, propiedades, usos, clasificacion, dif_electronegatividad);
                molecules.Add(moleculeData);
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

        return molecules;
    }

    public List<MoleculeMappingData> GetAllMoleculeMappings()
    {
        List<MoleculeMappingData> molecules = new List<MoleculeMappingData>();
        //dejo un reader local para cada query, no siendo global
        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT * FROM moleculas_mapping_element;";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int idMol = reader.GetInt32(0);
                int idEle = reader.GetInt32(1);
                int amount = reader.GetInt32(2);
                MoleculeMappingData moleculeMapping = new MoleculeMappingData(idMol, idEle, amount);
                molecules.Add(moleculeMapping);
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

        return molecules;
    }

    public List<MoleculeData> GetAllMoleculesIn(List<int> moleculeIdList)
    {
        List<MoleculeData> molecules = new List<MoleculeData>();
        if (moleculeIdList != null && moleculeIdList.Count <= 0) return molecules;

        string idList = "(" + String.Join(",", moleculeIdList) + ")";

        SqliteDataReader reader = null;
        SqliteConnection dbConnection = null;

        try
        {
            string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional, caracteristicas, propiedades, usos, clasificacion, diferencia_electronegatividad " +
            "FROM moleculas_lista WHERE id IN " + idList + ";";

            //LLAMADA AL METODO DE LA DBMANAGER
            dbConnection = dBManager.openCon();
            reader = dBManager.ManageExec(dbConnection, sqlQuery);

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string formula = reader.GetString(1);
                string systematicNm = reader.GetString(2);
                string stockNm = reader.GetString(3);
                string traditionalNm = reader.GetString(4);
                string caracteristicas = dBManager.SafeGetString(reader, 5);
                string propiedades = dBManager.SafeGetString(reader, 6);
                string usos = dBManager.SafeGetString(reader, 7);
                string clasificacion = dBManager.SafeGetString(reader, 8);
                Nullable<float> dif_electronegatividad = dBManager.SafeGetFloat(reader, 9);

                MoleculeData moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm, caracteristicas, propiedades, usos, clasificacion, dif_electronegatividad);
                molecules.Add(moleculeData);
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

        return molecules;
    }
    #endregion
}
