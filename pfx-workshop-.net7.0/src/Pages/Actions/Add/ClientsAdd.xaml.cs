using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class ClientsAdd : Page
    {
        public ClientsAdd()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                { "phone", phone.Text }
            };

            string sqlQuery = "INSERT INTO public.\"Clients\" " +
                "(full_name, city, address, phone) " +
                "VALUES (@full_name, @city, @address, @phone);";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("src/Pages/Clients.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("src/Pages/Clients.xaml", UriKind.Relative));
        }
    }
}
