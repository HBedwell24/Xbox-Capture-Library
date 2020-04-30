using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XboxGameClipLibrary.ViewModels.ScreenshotViewModel;

namespace XboxGameClipLibrary.Views
{
    public partial class ScreenshotsPage : Page
    {
        private bool FilterHandle = true;

        public ScreenshotsPage()
        {
            // Initialize the CapturesPage component
            InitializeComponent();

            // Bind the capture data to the DataContext
            Loaded += CapturesPage_Loaded;
        }

        private void CapturesPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = new ScreenshotViewModel();

            // Unhook the Loaded method
            Loaded -= CapturesPage_Loaded;
        }

        private void ScreenshotListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView) sender;
            var dataContext = screenshotsPage.DataContext as ScreenshotViewModel;
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

        private void ScreenshotFilter_DropDownClosed(object sender, EventArgs e)
        {
            if (FilterHandle) HandleScreenshotFilter();
            FilterHandle = true;
        }

        private void ScreenshotFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            FilterHandle = !cmb.IsDropDownOpen;
            HandleScreenshotFilter();
        }

        // Gets input from comboboxes
        private void HandleScreenshotFilter()
        {
            var dataContext = screenshotsPage.DataContext as ScreenshotViewModel;

            switch (screenshotFilterBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Date":
                    var screenshotByDate = dataContext.Screenshots.OrderByDescending(o => o.DatePublished).ToList();
                    dataContext.Screenshots = screenshotByDate;
                    break;

                case "Game":
                    var screenshotByGame = dataContext.Screenshots.OrderBy(o => o.TitleName).ThenByDescending(x => x.DatePublished).ToList();
                    dataContext.Screenshots = screenshotByGame;
                    break;

                case "Likes":
                    var screenshotByLikes = dataContext.Screenshots.OrderByDescending(o => o.RatingCount).ToList();
                    dataContext.Screenshots = screenshotByLikes;
                    break;

                case "Views":
                    var screenshotByViews = dataContext.Screenshots.OrderByDescending(o => o.Views).ToList();
                    dataContext.Screenshots = screenshotByViews;
                    break;
            }
        }

        public async void Refresh_List_View(object sender, RoutedEventArgs e)
        {
            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = new ScreenshotViewModel();
        }
    }
}