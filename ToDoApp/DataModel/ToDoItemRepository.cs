using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ToDoApp.Services;

namespace ToDoApp.DataModel;

public class ToDoItemRepository
{
    // access DbConnection class (dependency injection)
    private DbConnection _dbConnection;
    public ToDoItemRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public void AddTask(ToDoItem toDoItem)
    {
        _dbConnection.OpenConnection();
        using (SQLiteCommand cmd = new SQLiteCommand(_dbConnection.GetConnection()))
        {
            cmd.CommandText = "INSERT INTO Tasks (TaskName) VALUES (@TaskName)";
            cmd.Parameters.AddWithValue("@TaskName", toDoItem.Name);
            // Execute the SQL command
            cmd.ExecuteNonQuery();
        }
        _dbConnection.CloseConnection();
    }

    public void DeleteTask(ToDoItem toDoItem)
    {
        try
        {
            _dbConnection.OpenConnection();
            using (SQLiteCommand cmd = new SQLiteCommand(_dbConnection.GetConnection()))
            {
                cmd.CommandText = "DELETE FROM Tasks WHERE TaskID = @TaskID";
                cmd.Parameters.AddWithValue("@TaskID", toDoItem.TaskID); // Assuming TaskID is the primary key
                cmd.ExecuteNonQuery(); // Use ExecuteNonQuery to perform the delete operation
            }
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine($"Error deleting task: {ex.Message}");
            throw; // Rethrow the exception to signal that the delete operation failed
        }
        finally
        {
            _dbConnection.CloseConnection();
        }
    }

    public IEnumerable<ToDoItem> GetAllTasks()
    {
        _dbConnection.OpenConnection();
        using (SQLiteCommand cmd = new SQLiteCommand(_dbConnection.GetConnection()))
        {
            cmd.CommandText = "SELECT * FROM Tasks";
            List<ToDoItem> tasks = new List<ToDoItem>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tasks.Add(new ToDoItem
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        Name = reader["TaskName"].ToString(),
                        isChecked = false
                    });
                }
            }
            return tasks;
        }
    }
}