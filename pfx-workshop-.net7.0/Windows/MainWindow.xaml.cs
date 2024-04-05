using pfx_workshop_.net7._0.Scripts.Other;
using pfx_workshop_.net7._0.Scripts.pfx_workshop_.net7._0.Scripts;
using pfx_workshop_.net7._0.Windows;
using System.Windows;

namespace pfx_workshop_.net7._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Pages/Startup.xaml", UriKind.Relative)); // Стартовая страница

            this.EnableDragMoveForLabel("pfx_label"); // Перемещение окна
        }

        // WindowExtensions.cs
        // Смена темы
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme(this);
        }

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


        // МЕНЮ
        // Страница Заказы
        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Orders.xaml", UriKind.Relative));
        }

        // Страница Педали
        private void PedalsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Pedals.xaml", UriKind.Relative));
        }

        // Страница Клиенты
        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        // Страница Склад
        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Warehouse.xaml", UriKind.Relative));
        }

        // Страница Запчасти
        private void PartsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Parts.xaml", UriKind.Relative));
        }

        // Страница Поставщики
        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Suppliers.xaml", UriKind.Relative));
        }

        // SSH
        private void SSHButton_Click(object sender, RoutedEventArgs e)
        {
            SSHWindow sshWindow = new();
            sshWindow.Show();
        }

        // Перезапуск базы данных
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.RestartDatabase();
        }
    }
}