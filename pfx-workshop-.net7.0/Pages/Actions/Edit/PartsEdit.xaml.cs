using System.Windows.Controls;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class PartsEdit : Page
    {
        public PartsEdit()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*            // Предикт isNull и ошибок
                        if (string.IsNullOrWhiteSpace(item_name.Text))
                        {
                            MessageBox.Show("Значение наименования не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(quantity.Text))
                        {
                            MessageBox.Show("Значение количества не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        if (!int.TryParse(quantity.Text, out int quantityValue))
                        {
                            MessageBox.Show("Значение количества должно быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(supplier.Text))
                        {
                            MessageBox.Show("Значение поставщика не может быть пустым.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        if (!int.TryParse(supplier.Text, out int supplierValue))
                        {
                            MessageBox.Show("Значение поставщика должно быть целым числом.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }


                        // Атрибуты таблицы и их привязка к textbox
                        Dictionary<string, object> textBoxValues = new()
                        {
                            { "item_name", item_name.Text },
                            { "quantity", quantityValue },
                            { "supplier", supplierValue },
                        };

                        string sqlQuery = "INSERT INTO public.\"Parts\" " +
                            "(item_name, quantity, supplier) " +
                            "VALUES (@item_name, @quantity, @supplier);";
                        DataHelper.CreateTable(sqlQuery, textBoxValues);

                        NavigationService.Navigate(new Uri("Pages/Parts.xaml", UriKind.Relative));
                    }

                    private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
                    {
                        NavigationService.Navigate(new Uri("Pages/Parts.xaml", UriKind.Relative));
                    }

                    private static readonly Regex _regex = MyRegex();

                    private void CheckIsInteger(object sender, TextChangedEventArgs e)
                    {
                        var textBox1 = sender as TextBox;
                        quantity.Text = _regex.Replace(textBox1.Text, "");
                        var textBox2 = sender as TextBox;
                        supplier.Text = _regex.Replace(textBox2.Text, "");
                    }

                    [GeneratedRegex("[^0-9]+")]
                    private static partial Regex MyRegex();*/
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Parts.xaml", UriKind.Relative));
        }
    }
}
