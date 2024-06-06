using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class Parts : Page
  {
    public Parts()
    {
      InitializeComponent();

      /* Предварительная подгрузка данных из таблицы */
      LoadPartsData();
    }

    /* CRUD Операции
    * Добавление (Create) */
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Actions/Add/PartsAdd.xaml", UriKind.Relative));
    }
    /* Чтение (Read) */
    private void LoadPartsData()
    {
      string sqlQuery = "SELECT Parts.p_id, Parts.item_name, Parts.quantity, Suppliers.name " +
                "FROM public.\"Parts\" Parts " +
                "JOIN public.\"Suppliers\" Suppliers ON Parts.supplier = Suppliers.s_id;";
      DataTable clientsData = DataHelper.ReadTable(sqlQuery);

      partsDataGrid.ItemsSource = clientsData.DefaultView;
    }
    /* Редактирование (Update) */
    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button button)
      {
        if (button.DataContext is DataRowView rowView)
        {
          var partId = rowView["p_id"];

          NavigationService.Navigate(new Uri($"src/Pages/Actions/Edit/PartsEdit.xaml?p_id={partId}", UriKind.Relative));
        }
      }
    }
    /* Удаление (Delete) */
    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (partsDataGrid.SelectedItem != null)
      {
        DataRowView selectedRow = (DataRowView)partsDataGrid.SelectedItem;
        int partId = Convert.ToInt32(selectedRow["p_id"]);

        string sqlQuery = "DELETE FROM public.\"Parts\" WHERE p_id = @id";
        DataHelper.DeleteTable(sqlQuery, partId);

        LoadPartsData();
      }
    }


    /* Функции данных
     * Обновить данные в таблице */
    private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      LoadPartsData();
    }
    /* Поиск данных в таблице */
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchText = SearchBox.Text.ToLower();
      string sqlQuery = "SELECT * FROM public.\"Parts\" " +
          "LEFT JOIN public.\"Suppliers\" ON public.\"Parts\".p_id = public.\"Suppliers\".s_id " +
          "WHERE public.\"Parts\".p_id::text ILIKE @searchText " +
          "OR public.\"Parts\".item_name ILIKE @searchText " +
          "OR public.\"Parts\".quantity::text ILIKE @searchText " +
          "OR public.\"Suppliers\".name ILIKE @searchText;";
      DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

      partsDataGrid.ItemsSource = searchResults.DefaultView;
    }
  }
}
