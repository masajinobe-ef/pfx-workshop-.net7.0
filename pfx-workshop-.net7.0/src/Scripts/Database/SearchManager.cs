using Npgsql;
using System.Data;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts.DataBase
{
  public class SeachManager
  {
    /* Менеджер поиска данных */
    public static DataTable ReadTableWithSearch(string sqlQuery, string searchText)
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
            cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");
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
        MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      return dataTable;
    }
  }
}
