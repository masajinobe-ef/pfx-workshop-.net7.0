using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class ClientsEdit : Page
    {
        private int clientId;

        public ClientsEdit(int c_id)
        {
            InitializeComponent();
            clientId = c_id;
            PopulateFields();
        }

        private void PopulateFields()
        {
            try
            {
                string sqlQuery = $"SELECT full_name, city, address, phone FROM Clients WHERE c_id = {clientId}";
                DataTable dataTable = DataHelper.ReadTable(sqlQuery);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    full_name.Text = row["full_name"].ToString();
                    city.Text = row["city"].ToString();
                    address.Text = row["address"].ToString();
                    phone.Text = row["phone"].ToString();
                }
                else
                {
                    MessageBox.Show("Данные клиента не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при заполнении полей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(full_name.Text)
                || string.IsNullOrWhiteSpace(city.Text)
                || string.IsNullOrWhiteSpace(address.Text)
                || string.IsNullOrWhiteSpace(phone.Text))
            {
                MessageBox.Show("Значения ФИО, города, адреса и телефона не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Dictionary<string, object> textBoxValues = new()
            {
                { "full_name", full_name.Text },
                { "city", city.Text },
                { "address", address.Text },
                { "phone", phone.Text },
                { "id", clientId }
            };

            string sqlQuery = "UPDATE public.\"Clients\" " +
                "SET full_name = @full_name, city = @city, address = @address, phone = @phone " +
                "WHERE c_id = @id;";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }
    }
}

