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
            Menu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CameraSolid }, Text = "Screenshots", NavigationDestination = new ScreenshotsPage() });
            Menu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.VideoSolid }, Text = "Game Clips", NavigationDestination = new GameClipsPage() });

            OptionsMenu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogsSolid }, Text = "Settings", NavigationDestination = new SettingsPage() });
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
