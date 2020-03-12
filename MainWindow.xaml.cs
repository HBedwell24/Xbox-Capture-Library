using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models.Profile;

namespace XboxGameClipLibrary
{
    public partial class MainWindow : Window
    {
        CancellationToken cancellationToken;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindGameClipList();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
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

            // Bind the username to element 'Username' located in the XAML
            Username.DataContext = new Profile()
            {
                UserName = profile["gamerTag"].ToString()
            };

            // Bind the profile picture to element 'ProfileUri' located in the XAML
            ProfileUri.DataContext = new Profile()
            {
                ProfilePicUri = profile["imageUrl"].ToString()
            };
        }
    }
}
