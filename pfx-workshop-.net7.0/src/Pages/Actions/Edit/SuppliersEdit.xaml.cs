using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class SuppliersEdit : Page
  {
    private string supplierId;

    public SuppliersEdit()
    {
      InitializeComponent();

      this.Loaded += SuppliersEdit_Loaded;
    }

    private void SuppliersEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("s_id", out var id))
        {
          supplierId = id;
          LoadSupplierData(supplierId);
        }
      }
    }

    private void LoadSupplierData(string supplierId)
    {
      try
      {
        string sqlQuery = $"SELECT name, website, description FROM public.\"Suppliers\" WHERE s_id = {supplierId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          name.Text = row["name"].ToString();
          website.Text = row["website"].ToString();
          description.Text = row["description"].ToString();
        }
        else
        {
          MessageBox.Show("Данные поставщика не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
      if (string.IsNullOrWhiteSpace(name.Text)
          || string.IsNullOrWhiteSpace(website.Text)
          || string.IsNullOrWhiteSpace(description.Text))
      {
        MessageBox.Show("Значения названия, сайта и описания не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "name", name.Text },
                { "website" , website.Text },
                { "description", description.Text },
                { "id", supplierId }
            };

      string sqlQuery = "UPDATE public.\"Suppliers\" " +
          "SET name = @name, website = @website, description = @description " +
          "WHERE s_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные поставщика успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        NavigationService.Navigate(new Uri("src/Pages/Suppliers.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных поставщика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Suppliers.xaml", UriKind.Relative));
    }
  }
}
