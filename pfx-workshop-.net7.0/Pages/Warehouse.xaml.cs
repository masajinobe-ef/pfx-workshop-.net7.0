using Npgsql;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace pfx_workshop_.net7._0.Pages
{
    public partial class Warehouse : Page
    {
        private readonly string connectionString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";

        public Warehouse()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM public.\"Warehouse\";";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        var dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        WarehouseDataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении данных из базы данных: " + ex.Message);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void WarehouseDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (sender is DataGrid grid)
                {
                    {
                        DataRowView rowView = (DataRowView)e.Row.Item;
                        DataRow row = rowView.Row;

                        string newValue = ((TextBox)e.EditingElement).Text;

                        var columnBinding = ((DataGridTextColumn)e.Column).Binding as Binding;

                        if (columnBinding != null)
                        {
                            string columnName = columnBinding.Path.Path;

                            int id = Convert.ToInt32(row["s_id"]);

                            try
                            {
                                using (var conn = new NpgsqlConnection(connectionString))
                                {
                                    conn.Open();
                                    using (NpgsqlCommand cmd = new())
                                    {
                                        cmd.Connection = conn;
                                        cmd.CommandText = $"UPDATE public.\"Supplier\" SET {columnName} = @newValue WHERE s_id = @id;";
                                        cmd.Parameters.AddWithValue("@newValue", newValue);
                                        cmd.Parameters.AddWithValue("@id", id);
                                        int rowsAffected = cmd.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Значение обновлено успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                                            Refresh();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Не удалось обновить значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка обновления значения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseDataGrid.SelectedItem is DataRowView row && row != null)
            {
                int id = Convert.ToInt32(row["s_id"]);

                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (NpgsqlCommand cmd = new())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "DELETE FROM public.\"Warehouse\" WHERE s_id = @id;";
                            cmd.Parameters.AddWithValue("@id", id);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Запись удалена успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                                Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Не удалось удалить запись.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления строки: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите строку.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}