using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XboxGameClipLibrary.ViewModels.CapturesViewModel;

namespace XboxGameClipLibrary.Views
{
    public partial class CapturesPage : Page
    {
        private bool captureTypeHandle = true;
        private bool screenshotFilterHandle = true;
        private bool gameClipFilterHandle = true;

        private CapturesViewModel cvm;

        public CapturesPage()
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

        private void ScreenshotListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView) sender;
            var dataContext = capturesPage.DataContext as CapturesViewModel;
            var screenshot = dataContext.Screenshots[list.Items.IndexOf(list.SelectedItem)];

            screenshotDetailPane.ScreenshotId = screenshot.ScreenshotId;
            screenshotDetailPane.ScreenshotUri = screenshot.ScreenshotUris[0].Uri;
            screenshotDetailPane.Game = screenshot.TitleName;
            screenshotDetailPane.Device = screenshot.DeviceType;
            screenshotDetailPane.DateTaken = screenshot.DateTaken.ToString();
            screenshotDetailPane.DatePublished = screenshot.DatePublished.ToString();

            screenshotDetailPane.Views = screenshot.Views.ToString();
            screenshotDetailPane.Likes = screenshot.RatingCount.ToString();

            capturesDetailView.Visibility = Visibility.Visible;
        }

        private void CaptureTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (captureTypeHandle) HandleCaptureTypeComboBoxSelection();
            captureTypeHandle = true;
        }

        private void CaptureTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            captureTypeHandle = !cmb.IsDropDownOpen;
            HandleCaptureTypeComboBoxSelection();
        }

        // Gets input from comboboxes
        private void HandleCaptureTypeComboBoxSelection()
        {
            switch (captureBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Screenshots":
                    gameClipFilter.Visibility = Visibility.Collapsed;
                    gameClipListView.Visibility = Visibility.Collapsed;
                    screenshotFilter.Visibility = Visibility.Visible;
                    screenshotListView.Visibility = Visibility.Visible;
                    break;

                case "Game clips":
                    screenshotListView.Visibility = Visibility.Collapsed;
                    capturesDetailView.Visibility = Visibility.Collapsed;
                    screenshotFilter.Visibility = Visibility.Collapsed;
                    gameClipListView.Visibility = Visibility.Visible;
                    gameClipFilter.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ScreenshotFilter_DropDownClosed(object sender, EventArgs e)
        {
            if (screenshotFilterHandle) HandleScreenshotFilter();
            screenshotFilterHandle = true;
        }

        private void ScreenshotFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            screenshotFilterHandle = !cmb.IsDropDownOpen;
            HandleScreenshotFilter();
        }

        // Gets input from comboboxes
        private void HandleScreenshotFilter()
        {
            switch (screenshotFilterBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "By date":
                    var screenshotByDate = cvm.Screenshots.OrderByDescending(o => o.DatePublished).ToList();
                    cvm.Screenshots = screenshotByDate;
                    break;

                case "By game":
                    var screenshotByGame = cvm.Screenshots.OrderBy(o => o.TitleName).ThenByDescending(x => x.DatePublished).ToList();
                    cvm.Screenshots = screenshotByGame;
                    break;

                case "By likes":
                    var screenshotByLikes = cvm.Screenshots.OrderByDescending(o => o.RatingCount).ToList();
                    cvm.Screenshots = screenshotByLikes;
                    break;

                case "By views":
                    var screenshotByViews = cvm.Screenshots.OrderByDescending(o => o.Views).ToList();
                    cvm.Screenshots = screenshotByViews;
                    break;
            }
        }

        private void GameClipFilter_DropDownClosed(object sender, EventArgs e)
        {
            if (gameClipFilterHandle) HandleGameClipFilter();
            gameClipFilterHandle = true;
        }

        private void GameClipFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            gameClipFilterHandle = !cmb.IsDropDownOpen;
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