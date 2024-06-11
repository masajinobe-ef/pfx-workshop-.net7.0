using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class SuppliersAdd : Page
  {
    public SuppliersAdd()
    {
      InitializeComponent();
    }

    private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(name.Text)
          || string.IsNullOrWhiteSpace(website.Text)
          || string.IsNullOrWhiteSpace(description.Text))
      {
        MessageBox.Show("Наименование, сайт, описание не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      Dictionary<string, object> textBoxValues = new()
            {
                { "name", name.Text },
                { "website", website.Text },
                { "description", description.Text }
            };
      string sqlQuery = "INSERT INTO public.\"Suppliers\" " +
          "(name, website, description) " +
          "VALUES (@name, @website, @description);";
      DataHelper.CreateTable(sqlQuery, textBoxValues);

      NavigationService.Navigate(new Uri("src/Pages/Suppliers.xaml", UriKind.Relative));
    }

    private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Suppliers.xaml", UriKind.Relative));
    }
  }
}
