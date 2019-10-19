using Mono.Data.Sqlite;
using System;
using System.Data;
using UnityEngine;

//servicio de conexión con la base de datos.
public class DBManager : MonoBehaviour
{
    private string connectionString;

    //al inciar, setea el path a la db
    void Awake()
    {
        //path donde se encuentra la base de datos, "Application.dataPath" es el path x default en donde guarda unity
        //  //Data Source cannot be empty.  Use :memory: to open an in-memory database 
        connectionString = "URI=file:" + Application.dataPath + "/SIAMM.db";
        //metodo para chequeo de la conexion
        checkDB();
    }

    #region Metodos Especial DB

    private void checkDB()
    {
        SqliteConnection dbConnection = null;

        try
        {
            dbConnection = openCon();
            dbConnection.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public SqliteDataReader ManageExec(SqliteConnection con, String Query)
    {
        SqliteDataReader reader = null;
        SqliteCommand dbCommand = null;

        try
        {
            dbCommand = getCMD(Query);
            reader = EjecutaConsultaSql(con, dbCommand);
        }
        catch (Exception e)
        {
            throw e;
        }

        return reader;
    }


    public void ManageClosing(SqliteConnection con, SqliteDataReader reader)
    {
        try
        {
            if (reader != null)
            reader.Close();

            if (con != null)
                con.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //METODO PRINCIPAL GENERAL DE APERTURA DE CONECCIONES
    public SqliteConnection openCon(String connectionString)
    {
        SqliteConnection con = new SqliteConnection();

        try
        {
            con = GetCnxDB(connectionString);
            con.Open();
        }
        catch (Exception e)
        {
            throw e;
        }
        return con;
    }

    //override
    public SqliteConnection openCon()
    {
        SqliteConnection con = new SqliteConnection();

        try
        {
            con = GetCnxDB(this.connectionString);
            con.Open();
        }
        catch (Exception e)
        {
            Debug.LogError("DBManager :: OpenConnection Error: " + e.ToString());
            throw e;
        }
        return con;
    }

    public string SafeGetString(SqliteDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetString(colIndex);
        return string.Empty;
    }

    public Nullable<float> SafeGetFloat(SqliteDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetFloat(colIndex);
        return null;//el null del c#
    }

    public Nullable<int> SafeGetInt(SqliteDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetInt32(colIndex);
        return null;//el null del c#
    }
    #endregion

    #region SqlLiteDbConnection

    public SqliteConnection GetCnxDB(String datoDb)
    {
        SqliteConnection cn = new SqliteConnection();
        try
        {
            cn.ConnectionString = datoDb;
        }
        catch (Exception e)
        {
            throw e;
        }
        return cn;
    }


    #endregion

    #region SqlLiteDbCommands
    public SqliteCommand getCMD(String script)
    {

        SqliteCommand aCommand = new SqliteCommand();
        aCommand.CommandText = script;
        aCommand.CommandType = CommandType.Text;

        return aCommand;
    }
    #endregion

    #region SqlConnectionCMDExec
    /*
     * Execute SQL query
     */
    public SqliteDataReader EjecutaConsultaSql(SqliteConnection cnparam, SqliteCommand cmd)
    {
        try
        {
            cmd.Connection = cnparam;
        }
        catch (Exception e)
        {
            throw e;
        }
        return cmd.ExecuteReader();
    }
    #endregion
}
