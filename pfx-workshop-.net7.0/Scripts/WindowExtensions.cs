using System.Windows;
using System.Windows.Input;

namespace pfx_workshop_.net7._0.Scripts
{
    public static class WindowExtensions
    {
        public static void MinimizeWindow(this Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

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

        public static void CloseWindow(this Window window)
        {
            window.Close();
        }

        public static void EnableDragMove(this Window window)
        {
            window.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                    window.DragMove();
            };
        }
    }
}