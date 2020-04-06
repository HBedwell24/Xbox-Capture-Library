using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XboxGameClipLibrary.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
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
