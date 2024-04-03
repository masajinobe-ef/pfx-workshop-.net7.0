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
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            pedalsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (pedalsDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)pedalsDataGrid.SelectedItem;
                int pedalId = Convert.ToInt32(selectedRow["pd_id"]);

                string sqlQuery = "DELETE FROM public.\"Pedals\" WHERE pd_id = @id";
                DataHelper.DeleteTable(sqlQuery, pedalId);
                LoadPedalsData();
            }
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPedalsData();
        }
    }
}
