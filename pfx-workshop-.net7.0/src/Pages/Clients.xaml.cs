using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class Clients : Page
  {
    public Clients()
    {
      InitializeComponent();

      /* Предварительная подгрузка данных из таблицы */
      LoadClientsData();
    }

    /* CRUD Операции
    * Добавление (Create) */
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Actions/Add/ClientsAdd.xaml", UriKind.Relative));
    }
    /* Чтение (Read) */
    private void LoadClientsData()
    {
      string sqlQuery = "SELECT * FROM public.\"Clients\"";
      DataTable clientsData = DataHelper.ReadTable(sqlQuery);

      clientsDataGrid.ItemsSource = clientsData.DefaultView;
    }
    /* Редактирование (Update) */
    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
      if (clientsDataGrid.SelectedItem != null)
      {
        DataRowView selectedRow = (DataRowView)clientsDataGrid.SelectedItem;
        int clientId = Convert.ToInt32(selectedRow["c_id"]);

        NavigationService.Navigate(new Uri($"Pages/Actions/Edit/ClientsEdit.xaml?c_id={clientId}", UriKind.Relative));
      }
    }
    /* Удаление (Delete) */
    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (clientsDataGrid.SelectedItem != null)
      {
        DataRowView selectedRow = (DataRowView)clientsDataGrid.SelectedItem;
        int clientId = Convert.ToInt32(selectedRow["c_id"]);

        string sqlQuery = "DELETE FROM public.\"Clients\" WHERE c_id = @id";
        DataHelper.DeleteTable(sqlQuery, clientId);

        LoadClientsData();
      }
    }


    /* Функции данных
     * Обновить данные в таблице */
    private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      LoadClientsData();
    }
    /* Поиск данных в таблице */
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchText = SearchBox.Text.ToLower();
      string sqlQuery = "SELECT * FROM public.\"Clients\" " +
          "WHERE c_id::text ILIKE @searchText " +
          "OR full_name ILIKE @searchText " +
          "OR city ILIKE @searchText " +
          "OR address ILIKE @searchText " +
          "OR phone ILIKE @searchText;";
      DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

      clientsDataGrid.ItemsSource = searchResults.DefaultView;
    }
  }
}
