using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class GetLogs
    {
        public static string RequestLogs()
        {
            PostgresLogger();
            return "Logs";
        }

        private static void PostgresLogger()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения логов: {ex.Message}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
