using Npgsql;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    namespace pfx_workshop_.net7._0.Scripts
    {
        public static class DatabaseManager
        {
            public static string RestartDatabase()
            {
                try
                {
                    ReloadConfig(); // Перезапуск конфига
                    return "Restarted";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return "Error";
                }
            }

            private static void ReloadConfig()
            {
                using (var connection = new NpgsqlConnection(ConnectionManager.GetConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT pg_reload_conf();", connection))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Сервер успешно перезапущен.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}