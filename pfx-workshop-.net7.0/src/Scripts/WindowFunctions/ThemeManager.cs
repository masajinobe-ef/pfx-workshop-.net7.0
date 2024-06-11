using System.IO;
using System.Windows;
using System.Windows.Media;

namespace pfx_workshop_.net7._0.Scripts.Other
{
  public static class ThemeManager
  {
    /* Менеджер смены тем */
    private static bool isDarkTheme = false;

    public static void ToggleTheme(Window window)
    {
      ResourceDictionary themeDictionary = new();

      try
      {
        if (isDarkTheme)
        {
          themeDictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml", UriKind.Absolute);
          isDarkTheme = false;
        }
        else
        {
          themeDictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml", UriKind.Absolute);
          isDarkTheme = true;
        }

        window.Resources.MergedDictionaries.Clear();
        window.Resources.MergedDictionaries.Add(themeDictionary);

        UpdateTheme(window);
      }
      catch (IOException ex)
      {
        MessageBox.Show($"Error loading theme: {ex.Message}");
      }
    }

    private static void UpdateTheme(Window window)
    {
      if (isDarkTheme)
      {
        window.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF303030"));
      }
      else
      {
        window.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
      }

      foreach (var child in LogicalTreeHelper.GetChildren(window))
      {
        if (child is FrameworkElement frameworkElement)
        {
          frameworkElement.Resources.MergedDictionaries.Clear();
          foreach (var dict in window.Resources.MergedDictionaries)
          {
            frameworkElement.Resources.MergedDictionaries.Add(dict);
          }
        }
      }
    }
  }
}
