using pfx_workshop_.net7._0.Scripts.DataBase;
using pfx_workshop_.net7._0.Scripts.Other;
using System.Windows;

namespace pfx_workshop_.net7._0
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();

            this.HandleEnterKeyPress(auth_button); // Вход по нажатию Enter
        }

        // ФУНКЦИИ ОКНА WindowExtensions.cs
        // Свернуть
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.MinimizeWindow();
        }

        // Развернуть
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleMaximizeWindow();
        }

        // Закрыть
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseWindow();
        }

        // АВТОРИЗАЦИЯ Authorization.cs
        // Вход
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            AuthManager.Auth(login.Text, password.Password, this);
        }
    }
}
