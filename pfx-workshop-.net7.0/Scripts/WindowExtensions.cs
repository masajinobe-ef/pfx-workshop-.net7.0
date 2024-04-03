using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class WindowExtensions
    {
        // Свернуть
        public static void MinimizeWindow(this Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        // Развернуть
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

        // Закрыть
        public static void CloseWindow(this Window window)
        {
            window.Close();
        }

        // Обработчик перемещения окна
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

        // Обработчик нажатий
        public static void HandleEnterKeyPress(this Window window, Button targetButton)
        {
            window.PreviewKeyDown += (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    targetButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }

                // ОБХОД АВТОРИЗАЦИИ!!!
                if (e.Key == Key.Space)
                {
                    new MainWindow().Show();
                    CloseWindow(window);
                }
            };
        }
    }
}