using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace pfx_workshop_.net7._0.Scripts.DataBase
{
    public static class AuthManager
    {
        public static void Auth(string login, string password, Window currentWindow)
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionManager.GetConnectionString()))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"Authorization\" WHERE login = @login AND password = @password;", connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            new MainWindow().Show();
                            currentWindow.Close();
                        }
                        else
                        {
                            if (currentWindow is Window window)
                            {
                                if (window.FindName("auth_label") is Label auth_label)
                                {
                                    auth_label.Content = "Ошибка авторизации!";
                                    auth_label.Foreground = Brushes.Red;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}