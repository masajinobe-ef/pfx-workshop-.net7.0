using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class PedalsAdd : Page
  {
    public PedalsAdd()
    {
      InitializeComponent();
    }

    private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(brand.Text)
          || string.IsNullOrWhiteSpace(name.Text)
          || string.IsNullOrWhiteSpace(description.Text)
          || string.IsNullOrWhiteSpace(price.Text)
          || string.IsNullOrWhiteSpace(category.Text))
      {
        MessageBox.Show("Бренд, наименование, описание, цена и категория не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      if (!int.TryParse(price.Text, out int priceValue))
      {
        MessageBox.Show("Цена должна быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "brand", brand.Text },
                { "name", name.Text },
                { "description", description.Text },
                { "price", priceValue },
                { "category", category.Text }
            };
      string sqlQuery = "INSERT INTO public.\"Pedals\" " +
          "(brand, name, description, price, category) " +
          "VALUES (@brand, @name, @description, @price, @category);";
      DataHelper.CreateTable(sqlQuery, textBoxValues);

      NavigationService.Navigate(new Uri("src/Pages/Pedals.xaml", UriKind.Relative));
    }

    private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
