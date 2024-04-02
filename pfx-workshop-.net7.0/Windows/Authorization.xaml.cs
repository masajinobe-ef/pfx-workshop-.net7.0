using pfx_workshop_.net7._0.Scripts;
using System.Windows;

namespace pfx_workshop_.net7._0
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            this.EnableDragMove(); // Drag window
        }

        // WindowExtensions

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.MinimizeWindow();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleMaximizeWindow();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseWindow();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            AuthManager.Auth(login.Text, password.Password, this);
        }
    }
}
