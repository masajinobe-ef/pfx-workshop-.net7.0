using System.IO;
using System.Text.Json;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class ConnectionManager
    {
        public static string GetConnectionString()
        {
            try
            {
                string jsonFilePath = "Settings/appsettings.json";

                if (!File.Exists(jsonFilePath))
                {
                    MessageBox.Show("Файл настроек не найден. Пожалуйста, убедитесь, что файл appsettings.json находится в папке Settings.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(0);
                }

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

                return $"Host={host};Port={port};Database={database};Username={username};Password={password};IncludeErrorDetail={error};";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка строки подключения из файла JSON: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }
    }
}