using System.IO;
using System.Text.Json;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class ConnectionManager
    {
        private static string? connectionString;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                LoadConnectionString();
            }

            return connectionString ?? throw new Exception("Строка подключения равна null.");
        }

        private static void LoadConnectionString()
        {
            try
            {
                string jsonFilePath = "Settings/appsettings.json";
                string jsonString = File.ReadAllText(jsonFilePath);

                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;

                var connectionStringElement = root.GetProperty("ConnectionString");
                var host = connectionStringElement.GetProperty("Host").GetString();
                var port = connectionStringElement.GetProperty("Port").GetString();
                var database = connectionStringElement.GetProperty("Database").GetString();
                var username = connectionStringElement.GetProperty("Username").GetString();
                var password = connectionStringElement.GetProperty("Password").GetString();
                var error = connectionStringElement.GetProperty("IncludeErrorDetail").GetString();

                connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};IncludeErrorDetail={error};";
            }
            catch (Exception ex)
            {
                connectionString = null;
                MessageBox.Show($"Ошибка при загрузке строки подключения из файла JSON: {ex.Message}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}