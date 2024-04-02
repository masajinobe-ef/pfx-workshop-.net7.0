﻿using pfx_workshop_.net7._0.Scripts;
using System.Data;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Clients : Page
    {
        public Clients()
        {
            InitializeComponent();
            LoadClientsData();
        }

        private void LoadClientsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Clients\"";
            DataTable clientsData = DataGridHelper.RetrieveDataFromDatabase(sqlQuery);

            clientsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadClientsData();
        }
    }
}
