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
            DataTable clientsData = DataGridHelper.RetrieveDataFromDatabase(sqlQuery);

            suppliersDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadSuppliersData();
        }
    }
}
