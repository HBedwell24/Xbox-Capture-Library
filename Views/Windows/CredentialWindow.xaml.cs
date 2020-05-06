using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace XboxCaptureLibrary
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
                SaveSettings();
                MainWindow mw = new MainWindow();
                mw.Show();
                Close();
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
