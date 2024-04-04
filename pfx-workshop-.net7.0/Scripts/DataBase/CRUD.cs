using Npgsql;
using System.Data;
using System.Windows;

namespace pfx_workshop_.net7._0.Scripts.DataBase
{
    public class DataHelper
    {
        // Добавление данных
        public static void CreateTable(string sqlQuery, Dictionary<string, object> textBoxValues)
        {
            string connectionString = ConnectionManager.GetConnectionString();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sqlQuery, connection))
                    {
                        foreach (var textBoxValue in textBoxValues)
                        {
                            cmd.Parameters.AddWithValue(textBoxValue.Key, textBoxValue.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
                MessageBox.Show($"Произошла ошибка при чтении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
