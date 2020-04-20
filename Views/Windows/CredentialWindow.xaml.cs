using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace XboxGameClipLibrary
{
    public partial class CredentialWindow : MetroWindow
    {
        public CredentialWindow()
        {
            InitializeComponent();
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.xboxApiKey = apiKey.Text;
            Properties.Settings.Default.Save();
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("^[a-z0-9]*$");

            if (!string.IsNullOrEmpty(apiKey.Text) && regex.IsMatch(apiKey.Text) && apiKey.Text.Length == 40)
            {
                SaveSettings();
                MainWindow mw = new MainWindow();
                mw.Show();
                Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            contentWrapper.Focus();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
