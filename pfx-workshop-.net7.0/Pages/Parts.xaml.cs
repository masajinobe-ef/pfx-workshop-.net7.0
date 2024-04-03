using pfx_workshop_.net7._0.Scripts;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Parts : Page
    {
        public Parts()
        {
            InitializeComponent();
            LoadPartsData();
        }

        // CRUD ОПЕРАЦИИ
        // Добавление данных
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate(new Uri("Pages/ClientAct.xaml", UriKind.Relative));
        }

        // Чтение данных
        private void LoadPartsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Parts\" " +
                "LEFT JOIN public.\"Suppliers\" ON public.\"Parts\".p_id = public.\"Suppliers\".s_id;";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            partsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        // Обновление данных
        /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
                {

                }*/

        // Удаление данных
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (partsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)partsDataGrid.SelectedItem;
                int partId = Convert.ToInt32(selectedRow["p_id"]);

                string sqlQuery = "DELETE FROM public.\"Parts\" WHERE p_id = @id";
                DataHelper.DeleteTable(sqlQuery, partId);

                LoadPartsData();
            }
        }


        // ДРУГИЕ ФУНКЦИИ
        // Обновление данных
        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPartsData();
        }

        // Поиск данных
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            string sqlQuery = "SELECT * FROM public.\"Parts\" " +
                "LEFT JOIN public.\"Suppliers\" ON public.\"Parts\".p_id = public.\"Suppliers\".s_id " +
                "WHERE public.\"Parts\".p_id::text ILIKE @searchText " +
                "OR public.\"Parts\".item_name ILIKE @searchText " +
                "OR public.\"Parts\".quantity::text ILIKE @searchText " +
                "OR public.\"Suppliers\".name ILIKE @searchText;";
            DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

            partsDataGrid.ItemsSource = searchResults.DefaultView;
        }
    }
}
