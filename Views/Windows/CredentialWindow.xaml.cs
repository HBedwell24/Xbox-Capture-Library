using MahApps.Metro.Controls;

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
            if (!string.IsNullOrEmpty(apiKey.ToString()))
            {
                Properties.Settings.Default.xboxApiKey = apiKey.Text;
                Properties.Settings.Default.Save();
            }
        }
        
        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveSettings();
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
