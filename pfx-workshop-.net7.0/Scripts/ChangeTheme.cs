using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class ThemeManager
    {
        public static string Switch()
        {
            ChangeTheme();
            return "ThemeChanged";
        }

        private static void ChangeTheme()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка смены темы: {ex.Message}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
