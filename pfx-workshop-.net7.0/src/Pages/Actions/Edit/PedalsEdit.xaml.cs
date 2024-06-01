using pfx_workshop_.net7._0.Scripts.DataBase;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class PedalsEdit : Page
    {
        public PedalsEdit()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Предикт isNull и ошибок
            if (string.IsNullOrWhiteSpace(brand.Text))
            {
                MessageBox.Show("Значение бренда не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Значение наименования не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(description.Text))
            {
                MessageBox.Show("Значение описания не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(price.Text))
            {
                MessageBox.Show("Значение цены не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(price.Text, out int priceValue))
            {
                MessageBox.Show("Значение количества должно быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(category.Text))
            {
                MessageBox.Show("Значение описания не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            // Атрибуты таблицы и их привязка к textbox
            Dictionary<string, object> textBoxValues = new()
            {
                { "brand", brand.Text },
                { "name", name.Text },
                { "description", description.Text },
                { "price", priceValue },
                { "category", category.Text }
            };

            string sqlQuery = "INSERT INTO public.\"Pedals\" " +
                "(brand, name, description, price, category) " +
                "VALUES (@brand, @name, @description, @price, @category);";
            DataHelper.CreateTable(sqlQuery, textBoxValues);

            NavigationService.Navigate(new Uri("Pages/Pedals.xaml", UriKind.Relative));
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Pedals.xaml", UriKind.Relative));
        }

        private static readonly Regex _regex = MyRegex();

        private void CheckIsInteger(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            price.Text = _regex.Replace(textBox.Text, "");
        }

        [GeneratedRegex("[^0-9]+")]
        private static partial Regex MyRegex();
    }
}
