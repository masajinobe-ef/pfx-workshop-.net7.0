using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class WarehouseAdd : Page
    {
        public WarehouseAdd()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(item_name.Text)
                || string.IsNullOrWhiteSpace(quantity.Text))
            {
                MessageBox.Show("Значения наименования, количества не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(quantity.Text, out int quantityValue))
            {
                MessageBox.Show("Значение количества должно быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Dictionary<string, object> textBoxValues = new()
            {
                { "item_name", item_name.Text },
                { "quantity", quantityValue }
            };

            string sqlQuery = "INSERT INTO public.\"Warehouse\" " +
                "(item_name, quantity) " +
                "VALUES (@item_name, @quantity);";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("Pages/Warehouse.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("src/Pages/Warehouse.xaml", UriKind.Relative));
        }
    }
}
