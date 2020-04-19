using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

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
        
        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
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
    }
}
