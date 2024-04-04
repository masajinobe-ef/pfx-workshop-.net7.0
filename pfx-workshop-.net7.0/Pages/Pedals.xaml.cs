﻿using pfx_workshop_.net7._0.Scripts;
using System.Data;
using System.Windows;
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

        // CRUD ОПЕРАЦИИ
        // Добавление данных
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Actions/PedalsAct.xaml", UriKind.Relative));
        }

        // Чтение данных
        private void LoadPedalsData()
        {
            string sqlQuery = "SELECT * FROM public.\"Pedals\"";
            DataTable clientsData = DataHelper.ReadTable(sqlQuery);

            pedalsDataGrid.ItemsSource = clientsData.DefaultView;
        }

        // Обновление данных
        /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
                {

                }*/

        // Удаление данных
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

        // ДРУГИЕ ФУНКЦИИ
        // Обновление данных
        private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPedalsData();
        }

        // Поиск данных
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            string sqlQuery = "SELECT * FROM public.\"Pedals\" " +
                "WHERE pd_id::text ILIKE @searchText " +
                "OR brand ILIKE @searchText " +
                "OR name ILIKE @searchText " +
                "OR description ILIKE @searchText " +
                "OR price::text ILIKE @searchText " +
                "OR category ILIKE @searchText;";
            DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

            pedalsDataGrid.ItemsSource = searchResults.DefaultView;
        }
    }
}
