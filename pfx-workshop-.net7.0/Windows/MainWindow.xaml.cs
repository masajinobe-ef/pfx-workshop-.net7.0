using pfx_workshop_.net7._0.Scripts;
using pfx_workshop_.net7._0.Scripts.pfx_workshop_.net7._0.Scripts;
using System.Windows;

namespace pfx_workshop_.net7._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Pages/Startup.xaml", UriKind.Relative)); // Startup page
            this.EnableDragMove(); // Drag window
        }

        // WindowExtensions

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.MinimizeWindow();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleMaximizeWindow();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseWindow();
        }

        // Menu

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Orders.xaml", UriKind.Relative));
        }

        private void PedalsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WarehouseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // Bottom menu

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.RestartDatabase();
        }
    }
}