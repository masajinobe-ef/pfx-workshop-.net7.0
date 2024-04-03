using pfx_workshop_.net7._0.Scripts;
using System.Data;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
            LoadClientsData();
        }

        private void LoadClientsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Orders\" " +
            "LEFT JOIN public.\"Clients\" ON public.\"Orders\".c_id = public.\"Clients\".c_id " +
            "LEFT JOIN public.\"Pedals\" ON public.\"Orders\".pd_id = public.\"Pedals\".pd_id;";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            ordersDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ordersDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ordersDataGrid.SelectedItem;
                int orderId = Convert.ToInt32(selectedRow["o_id"]);

                string sqlQuery = "DELETE FROM public.\"Orders\" WHERE o_id = @id";
                DataHelper.DeleteTable(sqlQuery, orderId);
                LoadClientsData();
            }
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadClientsData();
        }
    }
}
