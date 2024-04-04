using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class WarehouseAct : Page
    {
        public WarehouseAct()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Предикт isNull и ошибок
            if (string.IsNullOrWhiteSpace(item_name.Text))
            {
                MessageBox.Show("Значение наименования не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(quantity.Text))
            {
                MessageBox.Show("Значение количества не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(quantity.Text, out int quantityValue))
            {
                MessageBox.Show("Значение количества должно быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            // Атрибуты таблицы и их привязка к textbox
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
            NavigationService.Navigate(new Uri("Pages/Warehouse.xaml", UriKind.Relative));
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
