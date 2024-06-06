using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class WarehouseEdit : Page
  {
    private string warehouseId;

    public WarehouseEdit()
    {
      InitializeComponent();

      this.Loaded += WarehouseEdit_Loaded;
    }


    private void WarehouseEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("w_id", out var id))
        {
          warehouseId = id;
          LoadWarehouseData(warehouseId);
        }
      }
    }

    private void LoadWarehouseData(string clientId)
    {
      try
      {
        string sqlQuery = $"SELECT item_name, quantity FROM public.\"Warehouse\" WHERE w_id = {warehouseId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          item_name.Text = row["item_name"].ToString();
          quantity.Text = row["quantity"].ToString();
        }
        else
        {
          MessageBox.Show("Данные склада не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
      if (string.IsNullOrWhiteSpace(item_name.Text)
          || string.IsNullOrWhiteSpace(quantity.Text))
      {
        MessageBox.Show("Значения предмета и количества не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "item_name", item_name.Text },
                { "quantity", int.Parse(quantity.Text) },
                { "id", warehouseId }
            };

      string sqlQuery = "UPDATE public.\"Warehouse\" " +
          "SET item_name = @item_name, quantity = @quantity " +
          "WHERE w_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные склада успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        NavigationService.Navigate(new Uri("src/Pages/Warehouse.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Warehouse.xaml", UriKind.Relative));
    }

    private static readonly Regex _regex = MyRegex();

    private void CheckIsInteger(object sender, TextChangedEventArgs e)
    {
      var textBox = sender as TextBox;
      quantity.Text = _regex.Replace(textBox.Text, "");
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
  }
}
