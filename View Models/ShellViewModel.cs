using System;
using System.Linq;
using MahApps.Metro.IconPacks;
using XboxGameClipLibrary.Views;
using XboxGameClipLibrary.Views.Pages;

namespace XboxGameClipLibrary.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            Menu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CameraSolid }, Text = "Screenshots", NavigationDestination = new Uri("Views/Pages/ScreenshotsPage.xaml", UriKind.Relative) });
            Menu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.VideoSolid }, Text = "Game Clips", NavigationDestination = new Uri("Views/Pages/GameClipsPage.xaml", UriKind.Relative) });

            OptionsMenu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogsSolid }, Text = "Settings", NavigationDestination = new Uri("Views/Pages/SettingsPage.xaml", UriKind.Relative) });
        }

        public object GetItem(object uri)
        {
            return null == uri ? null : Menu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }

        public object GetOptionsItem(object uri)
        {
            return null == uri ? null : OptionsMenu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }
    }
}
