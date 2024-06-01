using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace pfx_workshop_.net7._0.Scripts.Other
{
  public static class WindowExtensions
  {
    /* Обработчик сворачивания окна */
    public static void MinimizeWindow(this Window window)
    {
      window.WindowState = WindowState.Minimized;
    }
    /* Обработчик развертывания окна */
    public static void ToggleMaximizeWindow(this Window window)
    {
      if (window.WindowState == WindowState.Maximized)
      {
        window.WindowState = WindowState.Normal;
      }
      else
      {
        window.WindowState = WindowState.Maximized;
      }
    }
    /* Обработчик закрытия окна */
    public static void CloseWindow(this Window window)
    {
      window.Close();
    }
    /* Обработчик перемещения окна */
    public static void EnableDragMoveForLabel(this Window window, string labelName)
    {
      if (window.FindName(labelName) is Label label)
      {
        label.MouseDown += (sender, e) =>
        {
          if (e.LeftButton == MouseButtonState.Pressed)
          {
            window.DragMove();
          }
        };
      }
    }
    /* Обработчик нажатий */
    public static void HandleEnterKeyPress(this Window window, Button targetButton)
    {
      window.PreviewKeyDown += (sender, e) =>
      {
        /* Обработчик нажатий входа */
        if (e.Key == Key.Enter)
        {
          targetButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
        }
        /* ОБХОД АВТОРИЗАЦИИ!!! */
        if (e.Key == Key.Space && Keyboard.IsKeyDown(Key.A))
        {
          new MainWindow().Show();
          window.CloseWindow();
        }
      };
    }
  }
}