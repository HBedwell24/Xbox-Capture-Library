using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
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

            // Navigate to the home page.
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new CapturesPage());
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
    }
}
