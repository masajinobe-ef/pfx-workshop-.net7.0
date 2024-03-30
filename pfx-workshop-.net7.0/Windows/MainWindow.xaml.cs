using pfx_workshop_.net7._0.Scripts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace pfx_workshop_.net7._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Pages/Startup.xaml", UriKind.Relative));
        }

        private void Label_Click(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Startup.xaml", UriKind.Relative));
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private bool isDarkMode = false;

        private void ChangeThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isDarkMode)
            {
                this.Background = new SolidColorBrush(Colors.White);
                isDarkMode = false;
            }
            else
            {
                this.Background = new SolidColorBrush(Colors.WhiteSmoke);
                isDarkMode = true;
            }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Products.xaml", UriKind.Relative));
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void SupplierButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Supplier.xaml", UriKind.Relative));
        }

        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Warehouse.xaml", UriKind.Relative));
        }

        private void RestartPostgresButton_Click(object sender, RoutedEventArgs e)
        {
            RestartManager.RestartPostgres();
        }

        private void RequestLogsButton_Click(object sender, RoutedEventArgs e)
        {
            GetLogs.RequestLogs();
        }
    }
}