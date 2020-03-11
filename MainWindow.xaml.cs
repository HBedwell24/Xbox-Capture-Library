using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models;

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

        private async void BindGameClipList()
        {
            cancellationToken = new CancellationToken();
            var xuid = await Task.Run(() => Api.GetXuidFromStringCallAsync(cancellationToken));
            var gameClips = await Task.Run(() => Api.GetGameClipsFromStreamCallAsync(cancellationToken, xuid));

            // Debug Xuid response
            Console.WriteLine("Xuid: " + xuid);

            // Debug GameClip response
            string jsonString = JsonConvert.SerializeObject(gameClips, Formatting.Indented);
            Console.WriteLine(jsonString);

            // Bind the data to element 'gameClips' located in the XAML
            GameClips.ItemsSource = gameClips;
        }
    }
}
