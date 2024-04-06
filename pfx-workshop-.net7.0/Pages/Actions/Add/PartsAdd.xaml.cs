using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class PartsAdd : Page
    {
        public PartsAdd()
        {
            InitializeComponent();
            PartsComboBox();
        }

        private void PartsComboBox()
        {
            string sqlQuery = "SELECT name FROM public.\"Suppliers\";";
            DataTable supplierDataTable = DataHelper.ReadTable(sqlQuery);

            supplierComboBox.ItemsSource = supplierDataTable?.DefaultView;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(item_name.Text)
                || string.IsNullOrWhiteSpace(quantity.Text)
                || supplierComboBox.SelectedItem == null)
            {
                MessageBox.Show("Значение наименования, количества и поставщика не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(quantity.Text, out int quantityValue))
            {
                MessageBox.Show("Значение количества должно быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (supplierComboBox.SelectedItem is not DataRowView selectedSupplier)
            {
                MessageBox.Show("Не удалось получить выбранного поставщика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? selectedSupplierName = selectedSupplier["name"] as string;

            if (string.IsNullOrWhiteSpace(selectedSupplierName))
            {
                MessageBox.Show("Не удалось получить имя выбранного поставщика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int supplierId = GetSupplierId(selectedSupplierName);

            if (supplierId == -1)
            {
                MessageBox.Show("Не удалось получить идентификатор поставщика.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dictionary<string, object> textBoxValues = new()
            {
                { "item_name", item_name.Text },
                { "quantity", quantityValue },
                { "supplier", supplierId }
            };

            string sqlQuery = "INSERT INTO public.\"Parts\" " +
                "(item_name, quantity, supplier) " +
                "VALUES (@item_name, @quantity, @supplier);";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService?.Navigate(new Uri("Pages/Parts.xaml", UriKind.Relative));
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/Parts.xaml", UriKind.Relative));
        }
    }
}
