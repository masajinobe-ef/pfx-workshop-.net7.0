using Npgsql;
using pfx_workshop_.net7._0.Scripts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace pfx_workshop_.net7._0
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionManager.GetConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"Authorization\" WHERE a_login = @login AND a_password = @password;", conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login.Text);
                        cmd.Parameters.AddWithValue("@password", password.Password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MainWindow MainWindow = new();
                            MainWindow.Show();
                            Close();
                        }
                        else
                        {
                            label.Content = "Ошибка авторизации!";
                            label.Foreground = Brushes.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных SQL: {ex.Message}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Auth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Auth_Click(sender, e);
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
