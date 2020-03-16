using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.ViewModels;
using MenuItem = XboxGameClipLibrary.ViewModels.MenuItem;
namespace XboxGameClipLibrary
{
    public partial class MainWindow : MetroWindow
    {
        CancellationToken cancellationToken;

        public MainWindow()
        {
            InitializeComponent();

            Navigation.Navigation.Frame = new Frame() { NavigationUIVisibility = NavigationUIVisibility.Hidden };
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;
            this.HamburgerMenuControl.Content = Navigation.Navigation.Frame;

            // Navigate to the home page.
            this.Loaded += new RoutedEventHandler(Page_Loaded);
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new Uri("Views/CapturesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //BindGameClipList();
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            this.HamburgerMenuControl.SelectedItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetItem(e.Uri);
            this.HamburgerMenuControl.SelectedOptionsItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetOptionsItem(e.Uri);
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            var menuItem = e.InvokedItem as MenuItem;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination, menuItem);
            }
        }

        private async void BindGameClipList()
        {
            cancellationToken = new CancellationToken();

            var profile = await Task.Run(() => Api.GetProfileFromStringCallAsync(cancellationToken));
            var xuid = profile["userXuid"].ToString();
            var gameClips = await Task.Run(() => Api.GetGameClipsFromStreamCallAsync(cancellationToken, xuid));

            // Debug Xuid response
            Console.WriteLine("Xuid: " + xuid);

            // Debug Profile response
            Console.WriteLine(profile);

            // Debug GameClip response
            string jsonString = JsonConvert.SerializeObject(gameClips, Formatting.Indented);
            Console.WriteLine(jsonString);
        }
    }
}
