using Renci.SshNet;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace pfx_workshop_.net7._0.Windows
{
    public partial class SSH : Window
    {
/*        private string host;
        private string username;
        private string password;
        private string keyPath;

        public SSH()
        {
            InitializeComponent();

            try
            {
                string jsonString = File.ReadAllText("Settings/appsettings.json");

                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;

                var sshElement = root.GetProperty("SSH");
                var host = sshElement.GetProperty("Host").GetString();
                var username = sshElement.GetProperty("Username").GetString();
                var password = sshElement.GetProperty("Password").GetString();
                var keyPath = sshElement.GetProperty("KeyPath").GetString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load SSH settings: " + ex.Message);
            }
        }

        private void SendCommand_Click(object sender, RoutedEventArgs e)
        {
            string command = CommandTextBox.Text;
            ExecuteSSHCommand(command);
        }

        private void ExecuteSSHCommand(string command)
        {
            try
            {
                var connectionInfo = new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password));

                if (!string.IsNullOrEmpty(keyPath))
                {
                    connectionInfo.AuthenticationMethods.Add(new PrivateKeyAuthenticationMethod(username, new PrivateKeyFile(keyPath)));
                }

                using (var client = new SshClient(connectionInfo))
                {
                    client.Connect();
                    SshCommand sshCommand = client.RunCommand(command);
                    OutputTextBox.Text += sshCommand.Result + Environment.NewLine;
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text += "Error: " + ex.Message + Environment.NewLine;
            }
        }*/
    }
}
