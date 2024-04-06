using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class SuppliersEdit : Page
    {
        public SuppliersEdit()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Предикт isNull и ошибок
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Значение наименование не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(website.Text))
            {
                MessageBox.Show("Значение сайта не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(description.Text))
            {
                MessageBox.Show("Значение описания не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            // Атрибуты таблицы и их привязка к textbox
            Dictionary<string, object> textBoxValues = new()
            {
                { "name", name.Text },
                { "website", website.Text },
                { "description", description.Text }
            };

            string sqlQuery = "INSERT INTO public.\"Suppliers\" " +
                "(name, website, description) " +
                "VALUES (@name, @website, @description);";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("Pages/Suppliers.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Suppliers.xaml", UriKind.Relative));
        }
    }
}
