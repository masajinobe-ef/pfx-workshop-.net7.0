using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class ClientsEdit : Page
  {
    private string clientId;

    public ClientsEdit()
    {
      InitializeComponent();

      this.Loaded += ClientsEdit_Loaded;
    }

    private void ClientsEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("c_id", out var id))
        {
          clientId = id;
          LoadClientData(clientId);
        }
      }
    }

    private void LoadClientData(string clientId)
    {
      try
      {
        string sqlQuery = $"SELECT full_name, city, address, phone FROM public.\"Clients\" WHERE c_id = {clientId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          full_name.Text = row["full_name"].ToString();
          city.Text = row["city"].ToString();
          address.Text = row["address"].ToString();
          phone.Text = row["phone"].ToString();
        }
        else
        {
          MessageBox.Show("Данные клиента не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при заполнении полей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
      if (string.IsNullOrWhiteSpace(full_name.Text)
          || string.IsNullOrWhiteSpace(city.Text)
          || string.IsNullOrWhiteSpace(address.Text)
          || string.IsNullOrWhiteSpace(phone.Text))
      {
        MessageBox.Show("ФИО, город, адрес и телефон не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "full_name", full_name.Text },
                { "city", city.Text },
                { "address", address.Text },
                { "phone", phone.Text },
                { "id", clientId }
            };
      string sqlQuery = "UPDATE public.\"Clients\" " +
          "SET full_name = @full_name, city = @city, address = @address, phone = @phone " +
          "WHERE c_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные клиента успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

        NavigationService.Navigate(new Uri("src/Pages/Clients.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Clients.xaml", UriKind.Relative));
    }
  }
}
