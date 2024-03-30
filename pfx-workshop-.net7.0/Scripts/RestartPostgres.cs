using Npgsql;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class RestartManager
    {
        public static string RestartPostgres()
        {
            ReloadConfig();
            return "Restarted";
        }

        private static void ReloadConfig()
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionManager.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT pg_reload_conf();", conn))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Сервер успешно перезапущен.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка перезагрузки сервера: {ex.Message}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}