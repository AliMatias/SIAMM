using UnityEngine;
using System.Data.SQLite;

public class DBConnector : MonoBehaviour
{
    void Start()
    {
        SQLiteConnection connection = getConnection();
        connection.Open();
        SQLiteCommand command = connection.CreateCommand();
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "CREATE TABLE IF NOT EXISTS 'elements' ( " +
                          "  'id' INTEGER PRIMARY KEY, " +
                          "  'name' TEXT NOT NULL, " +
                          "  'protons' INTEGER NOT NULL" +
                          "  'neutrons' INTEGER NOT NULL" +
                          "  'electrons' INTEGER NOT NULL" +
                          ");";
        command.ExecuteNonQuery();
        insertElements(connection);
        connection.Close();
    }

    private SQLiteConnection getConnection()
    {
        string path = Application.persistentDataPath + "/db/siamm.db";
        return new SQLiteConnection(@"Data Source=" + path + ";Version=3;");
    }

    private void insertElements(SQLiteConnection connection)
    {
        SQLiteCommand command = connection.CreateCommand();
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "INSERT INTO 'elements' " +
                            "VALUES(1,'hidrogeno',1,0,1)," +
                            "(2,'deuterio',1,1,1)," +
                            "(3,'tritio',1,2,1);";
        command.ExecuteNonQuery();
    }
    
}