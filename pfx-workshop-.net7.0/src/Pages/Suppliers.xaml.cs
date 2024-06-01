using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class Suppliers : Page
  {
    public Suppliers()
    {
      InitializeComponent();

      /* Предварительная подгрузка данных из таблицы */
      LoadSuppliersData();
    }

    /* CRUD Операции
    * Добавление (Create) */
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Actions/Add/SuppliersAdd.xaml", UriKind.Relative));
    }
    /* Чтение (Read) */
    private void LoadSuppliersData()
    {
      string sqlQuery = "SELECT * FROM public.\"Suppliers\"";
      DataTable clientsData = DataHelper.ReadTable(sqlQuery);

      suppliersDataGrid.ItemsSource = clientsData.DefaultView;
    }
    /* Редактирование (Update) */
    /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
            {

            }*/
    /* Удаление (Delete) */
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


    /* Функции данных
     * Обновить данные в таблице */
    private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      LoadSuppliersData();
    }
    /* Поиск данных в таблице */
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchText = SearchBox.Text.ToLower();
      string sqlQuery = "SELECT * FROM public.\"Suppliers\" " +
          "WHERE s_id::text ILIKE @searchText " +
          "OR name ILIKE @searchText " +
          "OR website ILIKE @searchText " +
          "OR description ILIKE @searchText;";
      DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

      suppliersDataGrid.ItemsSource = searchResults.DefaultView;
    }
  }
}
