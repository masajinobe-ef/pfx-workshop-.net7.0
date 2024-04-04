using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
            LoadOrdersData();
        }

        // CRUD ОПЕРАЦИИ
        // Добавление данных
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate(new Uri("Pages/ClientAct.xaml", UriKind.Relative));
        }

        // Чтение данных
        private void LoadOrdersData()
        {
            string sqlQuery = "SELECT * FROM public.\"Orders\" " +
            "LEFT JOIN public.\"Clients\" ON public.\"Orders\".c_id = public.\"Clients\".c_id " +
            "LEFT JOIN public.\"Pedals\" ON public.\"Orders\".pd_id = public.\"Pedals\".pd_id;";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            ordersDataGrid.ItemsSource = clientsData.DefaultView;
        }

        // Обновление данных
        /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
                {

                }*/

        // Удаление данных
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ordersDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ordersDataGrid.SelectedItem;
                int orderId = Convert.ToInt32(selectedRow["o_id"]);

                string sqlQuery = "DELETE FROM public.\"Orders\" WHERE o_id = @id";
                DataHelper.DeleteTable(sqlQuery, orderId);

                LoadOrdersData();
            }
        }


        // ДРУГИЕ ФУНКЦИИ
        // Обновление данных
        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadOrdersData();
        }

        // Поиск данных
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            string sqlQuery = "SELECT * FROM public.\"Orders\" " +
                "LEFT JOIN public.\"Clients\" ON public.\"Orders\".c_id = public.\"Clients\".c_id " +
                "LEFT JOIN public.\"Pedals\" ON public.\"Orders\".pd_id = public.\"Pedals\".pd_id " +
                "WHERE public.\"Orders\".o_id::text ILIKE @searchText " +
                "OR public.\"Clients\".full_name ILIKE @searchText " +
                "OR public.\"Clients\".city ILIKE @searchText " +
                "OR public.\"Clients\".address ILIKE @searchText " +
                "OR public.\"Clients\".phone ILIKE @searchText " +
                "OR public.\"Pedals\".name ILIKE @searchText " +
                "OR public.\"Pedals\".price::text ILIKE @searchText " +
                "OR public.\"Orders\".date ILIKE @searchText;";

            DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

            ordersDataGrid.ItemsSource = searchResults.DefaultView;
        }
    }
}
