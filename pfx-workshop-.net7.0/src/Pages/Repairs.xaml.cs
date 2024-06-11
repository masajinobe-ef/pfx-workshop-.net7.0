using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class Repairs : Page
  {
    public Repairs()
    {
      InitializeComponent();

      /* Предварительная подгрузка данных из таблицы */
      LoadRepairsData();
    }

    /* CRUD Операции
    * Добавление (Create) */
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Actions/Add/RepairsAdd.xaml", UriKind.Relative));
    }
    /* Чтение (Read) */
    private void LoadRepairsData()
    {
      string sqlQuery = "SELECT * FROM public.\"Repairs\";";
      DataTable repairsData = DataHelper.ReadTable(sqlQuery);

      repairsDataGrid.ItemsSource = repairsData.DefaultView;
    }
    /* Редактирование (Update) */
    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button button)
      {
        if (button.DataContext is DataRowView rowView)
        {
          var repairId = rowView["r_id"];

          NavigationService.Navigate(new Uri($"src/Pages/Actions/Edit/RepairsEdit.xaml?r_id={repairId}", UriKind.Relative));
        }
      }
    }
    /* Удаление (Delete) */
    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (repairsDataGrid.SelectedItem != null)
      {
        DataRowView selectedRow = (DataRowView)repairsDataGrid.SelectedItem;
        int repairId = Convert.ToInt32(selectedRow["r_id"]);

        string sqlQuery = "DELETE FROM public.\"Repairs\" WHERE r_id = @id;";
        DataHelper.DeleteTable(sqlQuery, repairId);

        LoadRepairsData();
      }
    }


    /* Функции данных
     * Обновить данные в таблице */
    private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      LoadRepairsData();
    }
    /* Поиск данных в таблице */
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchText = SearchBox.Text.ToLower();
      string sqlQuery = "SELECT * FROM public.\"Repairs\" " +
          "WHERE r_id::text ILIKE @searchText " +
          "OR master_name ILIKE @searchText " +
          "OR repair_date ILIKE @searchText " +
          "OR repair_reason ILIKE @searchText " +
          "OR repair_cost::text ILIKE @searchText " +
          "OR repair_status ILIKE @searchText;";
      DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

      repairsDataGrid.ItemsSource = searchResults.DefaultView;
    }
  }
}
