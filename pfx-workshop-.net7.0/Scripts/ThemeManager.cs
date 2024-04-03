using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class ThemeManager
    {
        private static bool isDarkTheme = false;

        // Смена темы
        public static void ToggleTheme(Window window)
        {
            ResourceDictionary rd = new();

            if (isDarkTheme)
            {
                rd.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml");
                isDarkTheme = false;
            }
            else
            {
                rd.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Pink.xaml");
                isDarkTheme = true;
            }

            if (window.Resources.MergedDictionaries.Count > 0)
            {
                window.Resources.MergedDictionaries[0] = rd;
            }
            else
            {
                window.Resources.MergedDictionaries.Add(rd);
            }

            // window.Background = isDarkTheme ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#212121")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        }
    }
}