using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
  public partial class Orders : Page
  {
    public Orders()
    {
      InitializeComponent();

      /* Предварительная подгрузка данных из таблицы */
      LoadOrdersData();
    }

    /* CRUD Операции
    * Добавление (Create) */
    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("src/Pages/Actions/Add/OrdersAdd.xaml", UriKind.Relative));
    }
    /* Чтение (Read) */
    private void LoadOrdersData()
    {
      string sqlQuery = "SELECT Orders.o_id AS o_id, Orders.c_id AS c_id, Orders.pd_id AS pd_id, Orders.date AS date, " +
                        "Clients.full_name AS full_name, Pedals.name AS name, Clients.city AS city, " +
                        "Clients.address AS address, Clients.phone AS phone, Pedals.price AS price " +
                        "FROM public.\"Orders\" Orders " +
                        "LEFT JOIN public.\"Clients\" Clients ON Orders.c_id = Clients.c_id " +
                        "LEFT JOIN public.\"Pedals\" Pedals ON Orders.pd_id = Pedals.pd_id;";
      DataTable clientsData = DataHelper.ReadTable(sqlQuery);

      ordersDataGrid.ItemsSource = clientsData.DefaultView;
    }
    /* Редактирование (Update) */
    /*        private void UpdateButton_Click(object sender, RoutedEventArgs e)
            {

            }*/
    /* Удаление (Delete) */
    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (ordersDataGrid.SelectedItem != null)
      {
        DataRowView selectedRow = (DataRowView)ordersDataGrid.SelectedItem;
        int orderId = Convert.ToInt32(selectedRow["o_id"]);

        string sqlQuery = "DELETE FROM public.\"Orders\" WHERE o_id = @id";
        DataHelper.DeleteTable(sqlQuery, orderId);

        LoadOrdersData();
      }
    }


    /* Функции данных
     * Обновить данные в таблице */
    private void RefreshButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      LoadOrdersData();
    }
    /* Поиск данных в таблице */
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchText = SearchBox.Text.ToLower();
      string sqlQuery = "SELECT * FROM public.\"Orders\" " +
          "LEFT JOIN public.\"Clients\" ON public.\"Orders\".c_id = public.\"Clients\".c_id " +
          "LEFT JOIN public.\"Pedals\" ON public.\"Orders\".pd_id = public.\"Pedals\".pd_id " +
          "WHERE public.\"Orders\".o_id::text ILIKE @searchText " +
          "OR public.\"Clients\".full_name ILIKE @searchText " +
          "OR public.\"Clients\".city ILIKE @searchText " +
          "OR public.\"Clients\".address ILIKE @searchText " +
          "OR public.\"Clients\".phone ILIKE @searchText " +
          "OR public.\"Pedals\".name ILIKE @searchText " +
          "OR public.\"Pedals\".price::text ILIKE @searchText " +
          "OR public.\"Orders\".date ILIKE @searchText;";

      DataTable searchResults = SeachManager.ReadTableWithSearch(sqlQuery, searchText);

      ordersDataGrid.ItemsSource = searchResults.DefaultView;
    }
  }
}
