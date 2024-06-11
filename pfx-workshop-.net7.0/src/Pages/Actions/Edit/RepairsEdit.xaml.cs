using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class RepairsEdit : Page
  {
    private string repairId;

    public RepairsEdit()
    {
      InitializeComponent();

      this.Loaded += RepairsEdit_Loaded;
    }

    private void RepairsEdit_Loaded(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null)
      {
        var uri = NavigationService.CurrentSource;
        var parameters = GetQueryParameters(uri.ToString());

        if (parameters.TryGetValue("r_id", out var id))
        {
          repairId = id;
          LoadRepairsData(repairId);
        }
      }
    }

    private void LoadRepairsData(string repairId)
    {
      try
      {
        string sqlQuery = $"SELECT repair_date, repair_reason, repair_cost, repair_status FROM public.\"Repairs\" WHERE r_id = {repairId}";
        DataTable dataTable = DataHelper.ReadTable(sqlQuery);

        if (dataTable.Rows.Count > 0)
        {
          DataRow row = dataTable.Rows[0];

          repair_date.Text = row["repair_date"].ToString();
          repair_reason.Text = row["repair_reason"].ToString();
          repair_cost.Text = row["repair_cost"].ToString();
          repair_status.Text = row["repair_status"].ToString();
        }
        else
        {
          MessageBox.Show("Данные ремонта не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
      if (string.IsNullOrWhiteSpace(repair_date.Text)
          || string.IsNullOrWhiteSpace(repair_reason.Text)
          || string.IsNullOrWhiteSpace(repair_cost.Text)
          || string.IsNullOrWhiteSpace(repair_status.Text))
      {
        MessageBox.Show("Дата, причина, цена и статус не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
          {
              { "repair_date", repair_date.Text },
              { "repair_reason", repair_reason.Text },
              { "repair_cost", int.Parse(repair_cost.Text) },
              { "repair_status", repair_status.Text },
              { "id", repairId }
          };
      string sqlQuery = "UPDATE public.\"Repairs\" " +
          "SET repair_date = @repair_date, repair_reason = @repair_reason, repair_cost = @repair_cost, repair_status = @repair_status " +
          "WHERE r_id = @id::integer;";

      try
      {
        DataHelper.UpdateTable(sqlQuery, textBoxValues);
        MessageBox.Show("Данные ремонта успешно обновлены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

        NavigationService.Navigate(new Uri("src/Pages/Repairs.xaml", UriKind.Relative));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Произошла ошибка при обновлении данных клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Repairs.xaml", UriKind.Relative));
    }

    private static readonly Regex _regex = MyRegex();

    private void CheckIsInteger(object sender, TextChangedEventArgs e)
    {
      var textBox = sender as TextBox;
      repair_cost.Text = _regex.Replace(textBox.Text, "");
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
  }
}
