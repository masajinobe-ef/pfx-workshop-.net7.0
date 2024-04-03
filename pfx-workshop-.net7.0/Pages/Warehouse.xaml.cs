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
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            warehouseDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (warehouseDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)warehouseDataGrid.SelectedItem;
                int warehouseId = Convert.ToInt32(selectedRow["w_id"]);

                string sqlQuery = "DELETE FROM public.\"Warehouse\" WHERE w_id = @id";
                DataHelper.DeleteTable(sqlQuery, warehouseId);
                LoadWarehouseData();
            }
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadWarehouseData();
        }
    }
}
