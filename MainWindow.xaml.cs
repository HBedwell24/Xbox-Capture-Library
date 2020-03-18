using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models;
using XboxGameClipLibrary.ViewModels;
using XboxGameClipLibrary.Views;
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
            this.HamburgerMenuControl.Content = Navigation.Navigation.Frame;

            // Create a CancellationTokenSource object
            CancellationTokenSource cts = new CancellationTokenSource();

            // Navigate to the home page.
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new CapturesPage(GetGameClips(cts.Token)));

            // Request cancellation.
            //cts.Cancel();

            // Cancellation should have happened, so call Dispose.
            //cts.Dispose();
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            this.HamburgerMenuControl.SelectedItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetItem(e.Uri);
            this.HamburgerMenuControl.SelectedOptionsItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetOptionsItem(e.Uri);
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            var menuItem = e.InvokedItem as MenuItemViewModel;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination, menuItem);
            }
        }

        private async Task<string> GetXuid(CancellationToken token)
        {
            var profile = await Task.Run(() => Api.GetProfileFromStringCallAsync(token));
            var xuid = profile["userXuid"].ToString();

            // Debug Profile response
            Console.WriteLine(profile);

            // Debug Xuid response
            Console.WriteLine("Xuid: " + xuid);

            return xuid;
        }

        private async Task<JObject> GetScreenshots(CancellationToken token)
        {
            var xuid = await GetXuid(token);
            var screenshots = await Task.Run(() => Api.GetScreenshotsFromStringCallAsync(token, xuid));

            // Debug Screenshot response
            Console.WriteLine(screenshots);

            return screenshots;
        }

        private async Task<List<GameClip>> GetGameClips(CancellationToken token)
        {
            var xuid = await GetXuid(token);
            var gameClips = await Task.Run(() => Api.GetGameClipsFromStreamCallAsync(token, xuid));
      
            // Debug GameClip response
            string jsonString = JsonConvert.SerializeObject(gameClips, Formatting.Indented);
            Console.WriteLine(jsonString);

            return gameClips;
        }
    }
}
