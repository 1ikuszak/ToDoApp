using System;
using System;
using System.Data;
using System.Data.SQLite;

namespace ToDoApp.Services;

public class DbConnection
{
    private SQLiteConnection _connection;
    public DbConnection(string connectionString)
    {
        _connection = new SQLiteConnection(connectionString);
    }
    
    public void OpenConnection()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    }
    
    public void CloseConnection()
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
    }

    public SQLiteConnection GetConnection()
    {
        return _connection;
    }
    
    public void CreateTasksTable()
    {
        OpenConnection();
        using (SQLiteCommand createTable = new SQLiteCommand(_connection))
        {
            createTable.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        TaskID INTEGER PRIMARY KEY AUTOINCREMENT,
                        TaskName TEXT
                    );";
            createTable.ExecuteNonQuery();
        }
        CloseConnection();
    }
    
}