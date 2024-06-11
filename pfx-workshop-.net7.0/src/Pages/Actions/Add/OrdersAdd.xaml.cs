using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class OrdersAdd : Page
  {
    public OrdersAdd()
    {
      InitializeComponent();

      ClientComboBox();
      PedalComboBox();

      date.Text = DateTime.Now.ToString();
    }

    private void ClientComboBox()
    {
      string sqlQuery = "SELECT full_name FROM public.\"Clients\";";
      DataTable clientDataTable = DataHelper.ReadTable(sqlQuery);

      clientComboBox.ItemsSource = clientDataTable?.DefaultView;
    }

    private void PedalComboBox()
    {
      string sqlQuery = "SELECT name FROM public.\"Pedals\";";
      DataTable pedalDataTable = DataHelper.ReadTable(sqlQuery);

      pedalComboBox.ItemsSource = pedalDataTable?.DefaultView;
    }

    private void AcceptButton_Click(object sender, RoutedEventArgs e)
    {
      if (clientComboBox.SelectedItem == null ||
          pedalComboBox.SelectedItem == null ||
          string.IsNullOrWhiteSpace(date.Text))
      {
        MessageBox.Show("Клиент, педаль и дата не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      if (clientComboBox.SelectedItem is not DataRowView selectedClient)
      {
        MessageBox.Show("Не удалось получить выбранного клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (pedalComboBox.SelectedItem is not DataRowView selectedPedal)
      {
        MessageBox.Show("Не удалось получить выбранную педаль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      string? selectedClientName = selectedClient["full_name"] as string;
      string? selectedPedalName = selectedPedal["name"] as string;

      if (string.IsNullOrWhiteSpace(selectedClientName) || string.IsNullOrWhiteSpace(selectedPedalName))
      {
        MessageBox.Show("Не удалось получить имя клиента или педали.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      int clientId = GetClientId(selectedClientName);
      int pedalId = GetPedalId(selectedPedalName);

      if (clientId == -1 || pedalId == -1)
      {
        MessageBox.Show("Не удалось получить идентификатор клиента или педали.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "c_id", clientId },
                { "pd_id", pedalId },
                { "date", date.Text }
            };
      string sqlQuery = "INSERT INTO public.\"Orders\" " +
          "(c_id, pd_id, date) " +
          "VALUES (@c_id, @pd_id, @date);";
      DataHelper.CreateTable(sqlQuery, textBoxValues);

      NavigationService.Navigate(new Uri("src/Pages/Orders.xaml", UriKind.Relative));
    }

    private static int GetClientId(string clientName)
    {
      string sqlQuery = $"SELECT c_id FROM public.\"Clients\" WHERE full_name = '{clientName}';";
      DataTable resultTable = DataHelper.ReadTable(sqlQuery);

      if (resultTable?.Rows.Count > 0)
      {
        return Convert.ToInt32(resultTable.Rows[0]["c_id"]);
      }
      else
      {
        return -1;
      }
    }

    private static int GetPedalId(string pedalName)
    {
      string sqlQuery = $"SELECT pd_id FROM public.\"Pedals\" WHERE name = '{pedalName}';";
      DataTable resultTable = DataHelper.ReadTable(sqlQuery);

      if (resultTable?.Rows.Count > 0)
      {
        return Convert.ToInt32(resultTable.Rows[0]["pd_id"]);
      }
      else
      {
        return -1;
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Orders.xaml", UriKind.Relative));
    }
  }
}
