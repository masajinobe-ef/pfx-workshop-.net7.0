using pfx_workshop_.net7._0.Scripts;
using System.Data;
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

        private void LoadSuppliersData()
        {
            string sqlQuery = "SELECT * FROM public.\"Suppliers\"";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            suppliersDataGrid.ItemsSource = clientsData.DefaultView;
        }

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

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadSuppliersData();
        }
    }
}
