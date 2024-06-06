using Npgsql;
using System.Data;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts.DataBase
{
  public class DataHelper
  {
    /* CRUD Операции
     * Добавление (Create) */
    public static void CreateTable(string sqlQuery, Dictionary<string, object> textBoxValues)
    {
      string connectionString = ConnectionManager.GetConnectionString();

      try
      {
        using (var connection = new NpgsqlConnection(connectionString))
        {
          connection.Open();

          using (var cmd = new NpgsqlCommand(sqlQuery, connection))
          {
            foreach (var textBoxValue in textBoxValues)
            {
              cmd.Parameters.AddWithValue(textBoxValue.Key, textBoxValue.Value);
            }

            cmd.ExecuteNonQuery();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при добавлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    /* Чтение (Read) */
    public static DataTable ReadTable(string sqlQuery)
    {
      DataTable dataTable = new();
      string connectionString = ConnectionManager.GetConnectionString();

      try
      {
        using (var connection = new NpgsqlConnection(connectionString))
        {
          connection.Open();

          using (var cmd = new NpgsqlCommand(sqlQuery, connection))
          {
            using (var reader = cmd.ExecuteReader())
            {
              if (reader.HasRows)
              {
                dataTable.Load(reader);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при чтении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return dataTable;
    }
    /* Редактирование (Update) */
    public static void UpdateTable(string sqlQuery, Dictionary<string, object> parameters)
    {
      string connectionString = ConnectionManager.GetConnectionString();

      try
      {
        using (var connection = new NpgsqlConnection(connectionString))
        {
          connection.Open();

          using (var cmd = new NpgsqlCommand(sqlQuery, connection))
          {
            foreach (var param in parameters)
            {
              cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
            }

            cmd.ExecuteNonQuery();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при редактировании записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    /* Удаление (Delete) */
    public static void DeleteTable(string sqlQuery, int id)
    {
      string connectionString = ConnectionManager.GetConnectionString();

      try
      {
        using (var connection = new NpgsqlConnection(connectionString))
        {
          connection.Open();

          using (var cmd = new NpgsqlCommand(sqlQuery, connection))
          {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
