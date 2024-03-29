﻿using MahApps.Metro;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace XboxCaptureLibrary.Views.Pages
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            if(Properties.Settings.Default.appTheme.Equals("darkTheme"))
            {
                appThemeToggle.IsChecked = true;
            }
            else if(Properties.Settings.Default.appTheme.Equals("lightTheme"))
            {
                appThemeToggle.IsChecked = false;
            }
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

        // Saves API key to settings
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

        // Listens for dark theme toggleButton state changes
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;

            if (toggleSwitch != null)
            {
                // If toggleButton is checked, dark theme is enabled
                if (toggleSwitch.IsChecked == true)
                {
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("DarkAccent"), ThemeManager.GetAppTheme("BaseDark"));
                    Properties.Settings.Default.appTheme = "darkTheme";
                    Properties.Settings.Default.Save();
                }
                // Else if toggleButton is not checked, light theme is enabled
                else
                {
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("LightAccent"), ThemeManager.GetAppTheme("BaseLight"));
                    Properties.Settings.Default.appTheme = "lightTheme";
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}
