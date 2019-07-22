﻿using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QryMoleculas : MonoBehaviour
{
    private DBManager dBManager = null;
    private UIPopup popup = null;

    public QryMoleculas()
    {
        dBManager = FindObjectOfType<DBManager>();
        popup = FindObjectOfType<UIPopup>();
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
            Debug.Log("Error en Metodo GetMoleculesByAtomNumberAndQuantity");
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Moleculas, cantidad de elementos");
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
            string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional " +
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
                moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm);
            }

        }
        catch (Exception e)
        {
            Debug.Log("Error en Metodo GetMoleculeById");
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Identificador de Molecula");
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
            Debug.Log("Error en Metodo GetUniqueElementCountInMoleculeById");
            popup.MostrarPopUp("Elementos Qry DB", "Error obteniendo Elementos Componentes de una Molecula");
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
                atomPositions.Add(new AtomInMolPositionData(id, moleculeId, elementId, posX,
                    posY, posZ, scale, connectedTo, connectionType));
            }
                
        }
        catch (Exception e)
        {
            Debug.Log("Error en Metodo GetElementPositions");
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo posiciones de los Elementos Quimicos");
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
            string sqlQuery = "SELECT id, formula, formula_nomenclatura_sistematica, nomenclatura_stock, nomenclatura_tradicional " +
            "FROM moleculas_lista;";

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
                MoleculeData moleculeData = new MoleculeData(id, formula, systematicNm, stockNm, traditionalNm);
                molecules.Add(moleculeData);
            }

        }
        catch (Exception e)
        {
            Debug.Log("Error en Metodo GetAllMolecules");
            popup.MostrarPopUp("Elementos Qry DB", "Error Obteniendo Todas las Moleculas de la Base");
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
