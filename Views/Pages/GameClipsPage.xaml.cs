using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using XboxCaptureLibrary.ViewModels.GameClipViewModel;

namespace XboxCaptureLibrary.Views
{
    public partial class GameClipsPage : Page
    {
        private bool FilterHandle = true;

        public GameClipsPage()
        {
            // Initialize the CapturesPage component
            InitializeComponent();

            // Bind the capture data to the DataContext
            Loaded += CapturesPage_Loaded;
        }

        private void CapturesPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = new GameClipViewModel();

            // Unhook the Loaded method
            Loaded -= CapturesPage_Loaded;
        }

        private void GameClipListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView) sender;
            var dataContext = gameClipsPage.DataContext as GameClipViewModel;
            var gameClip = dataContext.GameClips[list.Items.IndexOf(list.SelectedItem)];

            gameClipDetailPane.GameClipId = gameClip.GameClipId;
            gameClipDetailPane.GameClipUri = gameClip.GameClipUris[0].Uri;
            gameClipDetailPane.Game = gameClip.TitleName;
            gameClipDetailPane.Device = gameClip.DeviceType;
            gameClipDetailPane.Duration = gameClip.DurationInSeconds;
            gameClipDetailPane.DateRecorded = gameClip.DateRecorded.ToString();
            gameClipDetailPane.DatePublished = gameClip.DatePublished.ToString();

            gameClipDetailPane.Views = gameClip.Views.ToString();
            gameClipDetailPane.Likes = gameClip.RatingCount.ToString();

            gameClipDetailView.Visibility = Visibility.Visible;
        }

        private void GameClipFilter_DropDownClosed(object sender, EventArgs e)
        {
            if (FilterHandle) HandleGameClipFilter();
            FilterHandle = true;
        }

        private void GameClipFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            FilterHandle = !cmb.IsDropDownOpen;
            HandleGameClipFilter();
        }

        private void HandleGameClipFilter()
        {
            var dataContext = gameClipsPage.DataContext as GameClipViewModel;

            switch (gameClipFilterBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Date":
                    var gameClipsByDate = dataContext.GameClips.OrderByDescending(o => o.DatePublished).ToList();
                    dataContext.GameClips = gameClipsByDate;
                    break;

                case "Duration":
                    var gameClipsByDuration = dataContext.GameClips.OrderByDescending(o => o.DurationInSeconds).ToList();
                    dataContext.GameClips = gameClipsByDuration;
                    break;

                case "Game":
                    var gameClipsByGame = dataContext.GameClips.OrderBy(o => o.TitleName).ThenByDescending(x => x.DatePublished).ToList();
                    dataContext.GameClips = gameClipsByGame;
                    break;

                case "Likes":
                    var gameClipsByLikes = dataContext.GameClips.OrderByDescending(o => o.RatingCount).ToList();
                    dataContext.GameClips = gameClipsByLikes;
                    break;

                case "Views":
                    var gameClipsByViews = dataContext.GameClips.OrderByDescending(o => o.Views).ToList();
                    dataContext.GameClips = gameClipsByViews;
                    break;
            }
        }

        public async void Refresh_List_View(object sender, RoutedEventArgs e)
        {
            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = new GameClipViewModel();

            var dataContext = gameClipsPage.DataContext as GameClipViewModel;

            while (dataContext.GameClips == null)
            {
                await Task.Delay(1);
            }

            HandleGameClipFilter();
        }
    }
}