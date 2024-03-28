using Newtonsoft.Json;
using Npgsql;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace pfx_workshop_.net7._0
{
    public partial class Authorization : Window
    {
        private string? connectionString;

        public Authorization()
        {
            InitializeComponent();
            LoadConnectionString();
        }

        public void LoadConnectionString()
        {
            try
            {
                string jsonFilePath = "Settings/appsettings.json";
                string jsonString = File.ReadAllText(jsonFilePath);

                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;

                var connectionStringElement = root.GetProperty("ConnectionString");
                var host = connectionStringElement.GetProperty("Host").GetString();
                var port = connectionStringElement.GetProperty("Port").GetString();
                var database = connectionStringElement.GetProperty("Database").GetString();
                var username = connectionStringElement.GetProperty("Username").GetString();
                var password = connectionStringElement.GetProperty("Password").GetString();
                var error = connectionStringElement.GetProperty("IncludeErrorDetail").GetString();

                connectionString = $"Host={host};Port={port};Database={database};Username={username};IncludeErrorDetail={error};";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки строки подключения из файла JSON: " + ex.Message);
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

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
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
                MessageBox.Show("Ошибка получения данных SQL: " + ex.Message);

            }
        }

        private void Auth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Auth_Click(sender, e);
            }
        }
    }
}
