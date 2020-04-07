using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XboxGameClipLibrary.ViewModels.GameClipViewModel;

namespace XboxGameClipLibrary.Views
{
    public partial class GameClipsPage : Page
    {
        private bool FilterHandle = true;

        private GameClipViewModel gameClipViewModel;

        public GameClipsPage()
        {
            // Initialize the CapturesPage component
            InitializeComponent();

            // Bind the capture data to the DataContext
            Loaded += CapturesPage_Loaded;
        }

        private void CapturesPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Instantiate ViewModel
            gameClipViewModel = new GameClipViewModel();

            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = gameClipViewModel;

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
            switch (gameClipFilterBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "By date":
                    var gameClipsByDate = gameClipViewModel.GameClips.OrderByDescending(o => o.DatePublished).ToList();
                    gameClipViewModel.GameClips = gameClipsByDate;
                    break;

                case "By duration":
                    var gameClipsByDuration = gameClipViewModel.GameClips.OrderByDescending(o => o.DurationInSeconds).ToList();
                    gameClipViewModel.GameClips = gameClipsByDuration;
                    break;

                case "By game":
                    var gameClipsByGame = gameClipViewModel.GameClips.OrderByDescending(o => o.TitleName).ToList();
                    gameClipViewModel.GameClips = gameClipsByGame;
                    break;

                case "By likes":
                    var gameClipsByLikes = gameClipViewModel.GameClips.OrderByDescending(o => o.RatingCount).ToList();
                    gameClipViewModel.GameClips = gameClipsByLikes;
                    break;

                case "By views":
                    var gameClipsByViews = gameClipViewModel.GameClips.OrderByDescending(o => o.Views).ToList();
                    gameClipViewModel.GameClips = gameClipsByViews;
                    break;
            }
        }
    }
}