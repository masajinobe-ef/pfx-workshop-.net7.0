using pfx_workshop_.net7._0.Scripts;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class ClientsAct : Page
    {
        public ClientsAct()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Предикт isNull и ошибок
            if (string.IsNullOrWhiteSpace(full_name.Text))
            {
                MessageBox.Show("Значение ФИО не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(city.Text))
            {
                MessageBox.Show("Значение города не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(address.Text))
            {
                MessageBox.Show("Значение адреса не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(phone.Text))
            {
                MessageBox.Show("Значение телефона не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
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

            string sqlQuery = "INSERT INTO public.\"Clients\" " +
                "(full_name, city, address, phone) " +
                "VALUES (@full_name, @city, @address, @phone);";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Clients.xaml", UriKind.Relative));
        }
    }
}
