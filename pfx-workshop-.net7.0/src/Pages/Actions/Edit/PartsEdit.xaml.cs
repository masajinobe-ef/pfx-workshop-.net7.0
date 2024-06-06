using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class PartsEdit : Page
  {
/*    private string partId;*/

    public PartsEdit()
    {
      InitializeComponent();

/*      this.Loaded += PartsEdit_Loaded;

      SupplierComboBox();*/
    }

/*    private void SupplierComboBox()
    {
      string sqlQuery = "SELECT name FROM public.\"Suppliers\";";
      DataTable supplierDataTable = DataHelper.ReadTable(sqlQuery);

      supplierComboBox.ItemsSource = supplierDataTable?.DefaultView;
    }

    private static int GetSupplierId(string supplierName)
    {
      string sqlQuery = $"SELECT s_id FROM public.\"Suppliers\" WHERE name = '{supplierName}';";
      DataTable resultTable = DataHelper.ReadTable(sqlQuery);

      if (resultTable?.Rows.Count > 0)
      {
        return Convert.ToInt32(resultTable.Rows[0]["s_id"]);
      }
      else
      {
        return -1;
      }
    }

    private void PartsEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("p_id", out var id))
        {
          partId = id;
          LoadPartData(partId);
        }
      }
    }

    private void LoadPartData(string clientId)
    {
      try
      {
        string sqlQuery = $"SELECT item_name, quantity, supplier FROM public.\"Parts\" WHERE p_id = {partId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          item_name.Text = row["item_name"].ToString();
          quantity.Text = row["quantity"].ToString();
          supplier.Text = row["supplier"].ToString();
        }
        else
        {
          MessageBox.Show("Данные запчасти не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
          || string.IsNullOrWhiteSpace(quantity.Text)
          || string.IsNullOrWhiteSpace(supplier.Text))
      {
        MessageBox.Show("Значения названия, количества и поставщика не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
      {
                { "item_name", item_name.Text },
                { "quantity", int.Parse(quantity.Text) },
                { "supplier", int.Parse(supplier.Text) },
                { "id", partId }
            };

      string sqlQuery = "UPDATE public.\"Parts\" " +
          "SET item_name = @item_name, quantity = @quantity, supplier = @supplier " +
          "WHERE p_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные запчасти успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        NavigationService.Navigate(new Uri("src/Pages/Parts.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных запчасти: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
*/
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Parts.xaml", UriKind.Relative));
    }

/*    private static readonly Regex _regex = MyRegex();

    private void CheckIsInteger(object sender, TextChangedEventArgs e)
    {
      var textBox = sender as TextBox;
      quantity.Text = _regex.Replace(textBox.Text, "");
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();*/
  }
}
