using System.Windows;

namespace pfx_workshop_.net7._0.Scripts.Other
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
                rd.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Green.xaml");
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

            // window.Background = isDarkTheme ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            // UpdateLabelStyles(window);
        }

        /*        private static void UpdateLabelStyles(DependencyObject depObj)
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                    {
                        DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                        if (child is Button button)
                        {
                            button.Foreground = isDarkTheme ? Brushes.White : Brushes.Black;
                        }

                        if (child is Label label)
                        {
                            label.Foreground = isDarkTheme ? Brushes.Black : Brushes.White;
                            label.Background = isDarkTheme ? Brushes.White : Brushes.Black;
                        }

                        UpdateLabelStyles(child);
                    }
                }*/
    }
}