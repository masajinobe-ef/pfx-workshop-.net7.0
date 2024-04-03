using Npgsql;
using System.Data;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts
{
    public class DataHelper
    {
        // Чтение данных
        public static DataTable ReadTable(string sqlQuery)
        {
            DataTable dataTable = new();
            string connectionString = ConnectionManager.GetConnectionString();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return dataTable;
        }

        // Удаление данных
        public static void DeleteTable(string sqlQuery, int id)
        {
            string connectionString = ConnectionManager.GetConnectionString();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sqlQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
