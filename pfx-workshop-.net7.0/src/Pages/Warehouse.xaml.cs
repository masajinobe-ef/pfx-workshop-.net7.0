using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class Warehouse : Page
  {
    public Warehouse()
    {
      InitializeComponent();

      /* Предварительная подгрузка данных из таблицы */
      LoadWarehouseData();
    }

    /* CRUD Операции
    * Добавление (Create) */
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Actions/Add/WarehouseAdd.xaml", UriKind.Relative));
    }
    /* Чтение (Read) */
    private void LoadWarehouseData()
    {
      string sqlQuery = "SELECT * FROM public.\"Warehouse\";";
      DataTable clientsData = DataHelper.ReadTable(sqlQuery);

      warehouseDataGrid.ItemsSource = clientsData.DefaultView;
    }
    /* Редактирование (Update) */
    /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
            {

            }*/
    /* Удаление (Delete) */
    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (warehouseDataGrid.SelectedItem != null)
      {
        DataRowView selectedRow = (DataRowView)warehouseDataGrid.SelectedItem;
        int warehouseId = Convert.ToInt32(selectedRow["w_id"]);

        string sqlQuery = "DELETE FROM public.\"Warehouse\" WHERE w_id = @id;";
        DataHelper.DeleteTable(sqlQuery, warehouseId);

        LoadWarehouseData();
      }
    }


    /* Функции данных
     * Обновить данные в таблице */
    private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      LoadWarehouseData();
    }
    /* Поиск данных в таблице */
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchText = SearchBox.Text.ToLower();
      string sqlQuery = "SELECT * FROM public.\"Warehouse\" " +
          "WHERE w_id::text ILIKE @searchText " +
          "OR item_name ILIKE @searchText " +
          "OR quantity::text ILIKE @searchText;";
      DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

      warehouseDataGrid.ItemsSource = searchResults.DefaultView;
    }
  }
}
