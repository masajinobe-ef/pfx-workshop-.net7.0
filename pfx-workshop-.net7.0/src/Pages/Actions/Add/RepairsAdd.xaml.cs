using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class RepairsAdd : Page
  {
    public RepairsAdd()
    {
      InitializeComponent();

      repair_date.Text = DateTime.Now.ToString();
    }

    private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(repair_date.Text)
          || string.IsNullOrWhiteSpace(repair_reason.Text)
          || string.IsNullOrWhiteSpace(repair_cost.Text)
          || string.IsNullOrWhiteSpace(repair_status.Text))
      {
        MessageBox.Show("Дата, причина, цена и статус не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      if (!int.TryParse(repair_cost.Text, out int repaircostValue))
      {
        MessageBox.Show("Цена должна быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "repair_date", repair_date.Text },
                { "repair_reason", repair_reason.Text },
                { "repair_cost", repaircostValue },
                { "repair_status", repair_status.Text }
            };
      string sqlQuery = "INSERT INTO public.\"Repairs\" " +
          "(repair_date, repair_reason, repair_cost, repair_status) " +
          "VALUES (@repair_date, @repair_reason, @repair_cost, @repair_status);";
      DataHelper.CreateTable(sqlQuery, textBoxValues);

      NavigationService.Navigate(new Uri("src/Pages/Repairs.xaml", UriKind.Relative));
    }

    private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
