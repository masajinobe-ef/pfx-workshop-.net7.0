using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class ClientsEdit : Page
    {

        public ClientsEdit()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Предикт isNull и ошибок
            if (string.IsNullOrWhiteSpace(full_name.Text) || string.IsNullOrWhiteSpace(city.Text) || string.IsNullOrWhiteSpace(address.Text) || string.IsNullOrWhiteSpace(phone.Text))
            {
                MessageBox.Show("Значения ФИО, города, адреса и телефона не могут быть пустыми.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Атрибуты таблицы и их привязка к textbox
            Dictionary<string, object> textBoxValues = new()
                {
                    { "full_name", full_name.Text },
                    { "city", city.Text },
                    { "address", address.Text },
                    { "phone", phone.Text }
                };

            string sqlQuery = "UPDATE public.\"Clients\" " +
                "SET full_name = @full_name, city = @city, address = @address, phone = @phone " +
                "WHERE c_id = @id;";
            DataHelper.UpdateRecord(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }


        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }
    }
}

