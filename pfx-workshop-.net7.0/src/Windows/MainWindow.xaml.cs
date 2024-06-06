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

      /* Стартовая страница */
      MainFrame.Navigate(new Uri("src/Pages/Startup.xaml", UriKind.Relative));

      /* Перемещение окна */
      this.EnableDragMoveForLabel("pfx_label");
    }


    /* Scripts/WindowExtensions.cs
     * Смена темы */
    private void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
      ThemeManager.ToggleTheme(this);
    }
    /* Свернуть */
    private void MinButton_Click(object sender, RoutedEventArgs e)
    {
      this.MinimizeWindow();
    }
    /* Развернуть */
    private void MaxButton_Click(object sender, RoutedEventArgs e)
    {
      this.ToggleMaximizeWindow();
    }
    /* Закрыть */
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      this.CloseWindow();
    }


    /* Главное меню
     * Заказы */
    private void OrdersButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Orders.xaml", UriKind.Relative));
    }
    /* Педали */
    private void PedalsButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Pedals.xaml", UriKind.Relative));
    }
    /* Клиенты */
    private void ClientsButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Clients.xaml", UriKind.Relative));
    }
    /* Склад */
    private void WarehouseButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Warehouse.xaml", UriKind.Relative));
    }
    /* Запчасти */
    private void PartsButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Parts.xaml", UriKind.Relative));
    }
    /* Поставщики */
    private void SuppliersButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Suppliers.xaml", UriKind.Relative));
    }


    /* Дополнительное меню 
     * SSH */
    private void SSHButton_Click(object sender, RoutedEventArgs e)
    {
      SSH ssh = new();
      ssh.Show();
    }
    /* Перезагрузка конфига PostgreSQL */
    private void RestartButton_Click(object sender, RoutedEventArgs e)
    {
      DatabaseManager.RestartDatabase();
    }
    private void InfoButton_Click(object sender, RoutedEventArgs e)
    {
      MainFrame.Navigate(new Uri("src/Pages/Info.xaml", UriKind.Relative));
    }
  }
}