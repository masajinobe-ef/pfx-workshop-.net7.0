using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Suppliers : Page
    {
        public Suppliers()
        {
            InitializeComponent();
            LoadSuppliersData();
        }

        // CRUD ОПЕРАЦИИ
        // Добавление данных
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Actions/Add/SuppliersAdd.xaml", UriKind.Relative));
        }

        // Чтение данных
        private void LoadSuppliersData()
        {
            string sqlQuery = "SELECT * FROM public.\"Suppliers\"";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            suppliersDataGrid.ItemsSource = clientsData.DefaultView;
        }

        // Обновление данных
        /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
                {

                }*/

        // Удаление данных
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (suppliersDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)suppliersDataGrid.SelectedItem;
                int supplierId = Convert.ToInt32(selectedRow["s_id"]);

                string sqlQuery = "DELETE FROM public.\"Suppliers\" WHERE s_id = @id";
                DataHelper.DeleteTable(sqlQuery, supplierId);

                LoadSuppliersData();
            }
        }

        // ДРУГИЕ ФУНКЦИИ
        // Обновление данных
        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadSuppliersData();
        }

        // Поиск данных
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            string sqlQuery = "SELECT * FROM public.\"Suppliers\" " +
                "WHERE s_id::text ILIKE @searchText " +
                "OR name ILIKE @searchText " +
                "OR website ILIKE @searchText " +
                "OR description ILIKE @searchText;";
            DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

            suppliersDataGrid.ItemsSource = searchResults.DefaultView;
        }
    }
}
