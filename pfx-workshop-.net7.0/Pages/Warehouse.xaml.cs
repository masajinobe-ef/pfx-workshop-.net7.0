using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Warehouse : Page
    {
        public Warehouse()
        {
            InitializeComponent();
            LoadWarehouseData();
        }

        // CRUD ОПЕРАЦИИ
        // Добавление данных
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Actions/WarehouseAct.xaml", UriKind.Relative));
        }

        // Чтение данных
        private void LoadWarehouseData()
        {
            string sqlQuery = "SELECT * FROM public.\"Warehouse\";";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            warehouseDataGrid.ItemsSource = clientsData.DefaultView;
        }

        // Обновление данных
        /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
                {

                }*/

        // Удаление данных
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (warehouseDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)warehouseDataGrid.SelectedItem;
                int warehouseId = Convert.ToInt32(selectedRow["w_id"]);

                string sqlQuery = "DELETE FROM public.\"Warehouse\" WHERE w_id = @id;";
                DataHelper.DeleteTable(sqlQuery, warehouseId);

                LoadWarehouseData();
            }
        }


        // ДРУГИЕ ФУНКЦИИ
        // Обновление данных
        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadWarehouseData();
        }

        // Поиск данных
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            string sqlQuery = "SELECT * FROM public.\"Warehouse\" " +
                "WHERE w_id::text ILIKE @searchText " +
                "OR item_name ILIKE @searchText " +
                "OR quantity::text ILIKE @searchText;";
            DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

            warehouseDataGrid.ItemsSource = searchResults.DefaultView;
        }
    }
}
