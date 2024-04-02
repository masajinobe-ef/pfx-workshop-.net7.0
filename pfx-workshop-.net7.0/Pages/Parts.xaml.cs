using pfx_workshop_.net7._0.Scripts;
using System.Data;
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

        private void LoadPartsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Parts\" " +
            "LEFT JOIN public.\"Suppliers\" ON public.\"Parts\".p_id = public.\"Suppliers\".s_id;";
            DataTable clientsData = DataGridHelper.RetrieveDataFromDatabase(sqlQuery);

            partsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPartsData();
        }
    }
}
