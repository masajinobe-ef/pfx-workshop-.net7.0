using pfx_workshop_.net7._0.Scripts;
using System.Data;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Pedals : Page
    {
        public Pedals()
        {
            InitializeComponent();
            LoadPedalsData();
        }

        private void LoadPedalsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Pedals\"";
            DataTable clientsData = DataGridHelper.RetrieveDataFromDatabase(sqlQuery);

            pedalsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPedalsData();
        }
    }
}
