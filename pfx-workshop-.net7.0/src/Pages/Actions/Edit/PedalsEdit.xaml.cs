using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class PedalsEdit : Page
  {
    private string pedalId;

    public PedalsEdit()
    {
      InitializeComponent();

      this.Loaded += PedalsEdit_Loaded;
    }

    private void PedalsEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("pd_id", out var id))
        {
          pedalId = id;
          LoadPedalData(pedalId);
        }
      }
    }

    private void LoadPedalData(string pedalId)
    {
      try
      {
        string sqlQuery = $"SELECT brand, name, description, price, category FROM public.\"Pedals\" WHERE pd_id = {pedalId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          brand.Text = row["brand"].ToString();
          name.Text = row["name"].ToString();
          description.Text = row["description"].ToString();
          price.Text = row["price"].ToString();
          category.Text = row["category"].ToString();
        }
        else
        {
          MessageBox.Show("Данные педали не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
      if (string.IsNullOrWhiteSpace(brand.Text)
          || string.IsNullOrWhiteSpace(name.Text)
          || string.IsNullOrWhiteSpace(description.Text)
          || string.IsNullOrWhiteSpace(price.Text)
          || string.IsNullOrWhiteSpace(category.Text))
      {
        MessageBox.Show("Бренд, название, описание, цена и категория не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "brand", brand.Text },
                { "name", name.Text },
                { "description", description.Text },
                { "price", int.Parse(price.Text) },
                { "category", category.Text },
                { "id", pedalId }
            };
      string sqlQuery = "UPDATE public.\"Pedals\" " +
    "SET brand = @brand, name = @name, description = @description, price = @price, category = @category " +
    "WHERE pd_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные педали успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        NavigationService.Navigate(new Uri("src/Pages/Pedals.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных педали: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Pedals.xaml", UriKind.Relative));
    }

    private static readonly Regex _regex = MyRegex();

    private void CheckIsInteger(object sender, TextChangedEventArgs e)
    {
      var textBox = sender as TextBox;
      price.Text = _regex.Replace(textBox.Text, "");
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
  }
}
