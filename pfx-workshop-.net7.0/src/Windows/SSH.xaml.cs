using Renci.SshNet;
using System.Text;
using System.Windows;

namespace pfx_workshop_.net7._0.Windows
{
  public partial class SSH : Window
  {
    public SSH()
    {
      InitializeComponent();
    }

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        string host = hostTextbox.Text;
        int port = int.Parse(portTextbox.Text);
        string username = loginTextbox.Text;
        string password = passwordTextbox.Password;
        string command = commandTextbox.Text;

        using (var client = new SshClient(host, port, username, password))
        {
          await Task.Run(() =>
          {
            client.Connect();
          });

          Dispatcher.Invoke(() =>
          {
            returnTextbox.Text = "Подключение...";
          });

          if (client.IsConnected)
          {
            var commandResult = client.RunCommand(command);

            Dispatcher.Invoke(() =>
            {
              var result = Encoding.UTF8.GetString(Encoding.Default.GetBytes(commandResult.Result));
              returnTextbox.Text = result;
            });
          }
          else
          {
            Dispatcher.Invoke(() =>
            {
              returnTextbox.Text = "Ошибка: Подключение не удалось";
            });
          }

          await Task.Run(() =>
          {
            client.Disconnect();
          });
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Ошибка: " + ex.Message);
      }
    }
  }
}
