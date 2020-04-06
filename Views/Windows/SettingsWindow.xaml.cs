using System.Text.RegularExpressions;
using System.Windows;

namespace XboxGameClipLibrary.Views.Windows
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            currentKey.Content = Properties.Settings.Default.xboxApiKey;
        }

        public void Exit_Authorization_Detail_View()
        {
            authorizationDetail.Visibility = Visibility.Collapsed;
            authorizationSummary.Visibility = Visibility.Visible;
        }

        public void Edit_Click(object sender, RoutedEventArgs e)
        {
            authorizationSummary.Visibility = Visibility.Collapsed;
            authorizationDetail.Visibility = Visibility.Visible;
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Exit_Authorization_Detail_View();
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("^[a-z0-9]*$");

            if (!string.IsNullOrEmpty(apiKey.Text) && regex.IsMatch(apiKey.Text) && apiKey.Text.Length == 40)
            {
                Properties.Settings.Default.xboxApiKey = apiKey.Text;
                Properties.Settings.Default.Save();
                Exit_Authorization_Detail_View();
            }
        }
    }
}
