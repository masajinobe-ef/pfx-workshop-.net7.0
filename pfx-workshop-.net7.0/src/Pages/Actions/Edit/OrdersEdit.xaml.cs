using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class OrdersEdit : Page
  {
    private string orderId;

    public OrdersEdit()
    {
      InitializeComponent();

      ClientComboBox();
      PedalComboBox();

      this.Loaded += OrdersEdit_Loaded;
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

    private void OrdersEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("o_id", out var id))
        {
          orderId = id;
          LoadOrderData(orderId);
        }
      }
    }

    private void LoadOrderData(string orderId)
    {
      try
      {
        string sqlQuery = $"SELECT c_id, pd_id, date FROM public.\"Orders\" WHERE o_id = {orderId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          int clientId = Convert.ToInt32(row["c_id"]);
          int pedalId = Convert.ToInt32(row["pd_id"]);

          clientComboBox.SelectedValue = GetClientNameById(clientId);
          pedalComboBox.SelectedValue = GetPedalNameById(pedalId);
          date.Text = row["date"].ToString();
        }
        else
        {
          MessageBox.Show("Данные заказа не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при заполнении полей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private static string GetClientNameById(int clientId)
    {
      string sqlQuery = $"SELECT full_name FROM public.\"Clients\" WHERE c_id = {clientId};";
      DataTable resultTable = DataHelper.ReadTable(sqlQuery);

      if (resultTable?.Rows.Count > 0)
      {
        return resultTable.Rows[0]["full_name"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }

    private static string GetPedalNameById(int pedalId)
    {
      string sqlQuery = $"SELECT name FROM public.\"Pedals\" WHERE pd_id = {pedalId};";
      DataTable resultTable = DataHelper.ReadTable(sqlQuery);

      if (resultTable?.Rows.Count > 0)
      {
        return resultTable.Rows[0]["name"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }

    private static Dictionary<string, string> GetQueryParameters(string query)
    {
      var parameters = new Dictionary<string, string>();
      if (query.Contains('?'))
      {
        var queryString = query.Split('?')[1];
        var pairs = queryString.Split('&');
        foreach (var pair in pairs)
        {
          var keyValue = pair.Split('=');
          if (keyValue.Length == 2)
          {
            parameters[keyValue[0]] = keyValue[1];
          }
        }
      }
      return parameters;
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
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
                { "date", date.Text },
                { "id", orderId }
            };
      string sqlQuery = "UPDATE public.\"Orders\" " +
          "SET c_id = @c_id, pd_id = @pd_id, date = @date " +
          "WHERE o_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные заказа успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

        NavigationService.Navigate(new Uri("src/Pages/Orders.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Orders.xaml", UriKind.Relative));
    }
  }
}
