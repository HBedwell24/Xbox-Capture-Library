using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using XboxGameClipLibrary.API;

namespace XboxGameClipLibrary
{
    public partial class MainWindow : Window
    {
        CancellationToken cancellationToken;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindGameClipList();
        }

        private async void BindGameClipList()
        {
            cancellationToken = new CancellationToken();
            var gameClips = await Task.Run(() => Api.DeserializeOptimizedFromStreamCallAsync(cancellationToken));
            GameClips.ItemsSource = gameClips;
        }
    }
}
