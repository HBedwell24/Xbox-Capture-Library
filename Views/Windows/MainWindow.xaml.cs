using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using XboxGameClipLibrary.ViewModels;
using MenuItemViewModel = XboxGameClipLibrary.ViewModels.MenuItemViewModel;
namespace XboxGameClipLibrary
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Navigation.Navigation.Frame = new Frame() { NavigationUIVisibility = NavigationUIVisibility.Hidden };
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;
            HamburgerMenuControl.Content = Navigation.Navigation.Frame;

            // Navigate to the home page.
            Loaded += (sender, args) => Navigation.Navigation.Navigate(new Uri("Views/Pages/ScreenshotsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            HamburgerMenuControl.SelectedItem = e.ExtraData ?? ((ShellViewModel)DataContext).GetItem(e.Uri);
            HamburgerMenuControl.SelectedOptionsItem = e.ExtraData ?? ((ShellViewModel)DataContext).GetOptionsItem(e.Uri);
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            MenuItemViewModel menuItem = e.InvokedItem as MenuItemViewModel;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination, menuItem);
            }
        }

        private void AddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void ClearSettings()
        {
            Properties.Settings.Default.xboxApiKey = "";
            Properties.Settings.Default.Save();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            ClearSettings();
            CredentialWindow cw = new CredentialWindow();
            cw.Show();
            Close();
        }

        private void Navigate_To_Settings(object sender, RoutedEventArgs e)
        {
            Navigation.Navigation.Navigate(new Uri("Views/Pages/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
