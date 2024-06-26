﻿using Npgsql;
using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
  namespace pfx_workshop_.net7._0.Scripts
  {
    public static class DatabaseManager
    {
      /* Перезапуск базы данных */
      public static int RestartDatabase()
      {
        try
        {
          ReloadConfig();
          return 0;
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
          return 1;
        }
      }


      /* Перезапуск конфига */
      private static void ReloadConfig()
      {
        string connectionString = ConnectionManager.GetConnectionString();

        using (var connection = new NpgsqlConnection(connectionString))
        {
          connection.Open();

          using (var cmd = new NpgsqlCommand("SELECT pg_reload_conf();", connection))
          {
            cmd.ExecuteNonQuery();
            MessageBox.Show("База данных успешно перезапущена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
          }
        }
      }
    }
  }
}
