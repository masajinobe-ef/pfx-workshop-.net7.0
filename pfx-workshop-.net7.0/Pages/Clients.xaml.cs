using pfx_workshop_.net7._0.Scripts;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace pfx_workshop_.net7._0.Pages
{
    public partial class Clients : Page
    {
        public Clients()
        {
            InitializeComponent();
            LoadClientsData();
        }

        // CRUD ОПЕРАЦИИ
        // Добавление данных
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/ClientAct.xaml", UriKind.Relative));
        }

        // Чтение данных
        private void LoadClientsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Clients\"";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            clientsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        // Обновление данных
        /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
                {

                }*/

        // Удаление данных
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (clientsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)clientsDataGrid.SelectedItem;
                int clientId = Convert.ToInt32(selectedRow["c_id"]);

                string sqlQuery = "DELETE FROM public.\"Clients\" WHERE c_id = @id";
                DataHelper.DeleteTable(sqlQuery, clientId);

                LoadClientsData();
            }
        }


        // ДРУГИЕ ФУНКЦИИ
        // Обновление данных
        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadClientsData();
        }

        // Поиск данных
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            string sqlQuery = "SELECT * FROM public.\"Clients\" " + 
                "WHERE c_id::text ILIKE @searchText " +
                "OR full_name ILIKE @searchText " +
                "OR city ILIKE @searchText " +
                "OR address ILIKE @searchText " +
                "OR phone ILIKE @searchText;";
            DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

            clientsDataGrid.ItemsSource = searchResults.DefaultView;
        }
    }
}
