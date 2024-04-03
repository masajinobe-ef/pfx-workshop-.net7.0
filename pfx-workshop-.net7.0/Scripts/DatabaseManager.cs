using Npgsql;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    namespace pfx_workshop_.net7._0.Scripts
    {
        public static class DatabaseManager
        {
            // Перезапуск базы данных
            public static string RestartDatabase()
            {
                try
                {
                    ReloadConfig();
                    return "Restarted";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return "Error";
                }
            }

            // Перезапуск конфига
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
