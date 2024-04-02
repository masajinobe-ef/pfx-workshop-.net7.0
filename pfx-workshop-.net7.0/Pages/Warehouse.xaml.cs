using pfx_workshop_.net7._0.Scripts;
using System.Data;
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

        private void LoadWarehouseData()
        {
            string sqlQuery = "SELECT * FROM public.\"Warehouse\"";
            DataTable clientsData = DataGridHelper.RetrieveDataFromDatabase(sqlQuery);

            warehouseDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadWarehouseData();
        }
    }
}
