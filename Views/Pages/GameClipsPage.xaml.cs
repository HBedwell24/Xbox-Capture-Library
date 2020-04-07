using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XboxGameClipLibrary.ViewModels.CapturesViewModel;

namespace XboxGameClipLibrary.Views
{
    public partial class GameClipsPage : Page
    {
        private bool FilterHandle = true;

        private CapturesViewModel cvm;

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
            cvm = new CapturesViewModel();

            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = cvm;

            // Unhook the Loaded method
            Loaded -= CapturesPage_Loaded;
        }

        private void GameClipListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView) sender;
            var dataContext = gameClipsPage.DataContext as CapturesViewModel;
            var gameClip = dataContext.GameClips[list.Items.IndexOf(list.SelectedItem)];

            gameClipDetailPane.GameClipId = gameClip.GameClipId;
            gameClipDetailPane.GameClipUri = gameClip.GameClipUris[0].Uri;
            gameClipDetailPane.GameClipGame = gameClip.TitleName;
            gameClipDetailPane.GameClipDevice = gameClip.DeviceType;
            gameClipDetailPane.DateRecorded = gameClip.DateRecorded.ToString();
            gameClipDetailPane.GameClipDatePublished = gameClip.DatePublished.ToString();

            gameClipDetailPane.GameClipViews = gameClip.Views.ToString();
            gameClipDetailPane.GameClipLikes = gameClip.RatingCount.ToString();

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
                    var gameClipsByDate = cvm.GameClips.OrderByDescending(o => o.DatePublished).ToList();
                    cvm.GameClips = gameClipsByDate;
                    break;

                case "By duration":
                    var gameClipsByDuration = cvm.GameClips.OrderByDescending(o => o.DurationInSeconds).ToList();
                    cvm.GameClips = gameClipsByDuration;
                    break;

                case "By game":
                    var gameClipsByGame = cvm.GameClips.OrderByDescending(o => o.TitleName).ToList();
                    cvm.GameClips = gameClipsByGame;
                    break;

                case "By likes":
                    var gameClipsByLikes = cvm.GameClips.OrderByDescending(o => o.RatingCount).ToList();
                    cvm.GameClips = gameClipsByLikes;
                    break;

                case "By views":
                    var gameClipsByViews = cvm.GameClips.OrderByDescending(o => o.Views).ToList();
                    cvm.GameClips = gameClipsByViews;
                    break;
            }
        }
    }
}