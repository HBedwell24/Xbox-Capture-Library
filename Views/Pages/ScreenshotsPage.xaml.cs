using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XboxGameClipLibrary.ViewModels.ScreenshotViewModel;

namespace XboxGameClipLibrary.Views
{
    public partial class ScreenshotsPage : Page
    {
        private bool FilterHandle = true;
        private ScreenshotViewModel screenshotViewModel;

        public ScreenshotsPage()
        {
            // Initialize the CapturesPage component
            InitializeComponent();

            // Bind the capture data to the DataContext
            Loaded += CapturesPage_Loaded;
        }

        private void CapturesPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Instantiate ViewModel
            screenshotViewModel = new ScreenshotViewModel();

            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = screenshotViewModel;

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
            switch (screenshotFilterBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "By date":
                    var screenshotByDate = screenshotViewModel.Screenshots.OrderByDescending(o => o.DatePublished).ToList();
                    screenshotViewModel.Screenshots = screenshotByDate;
                    break;

                case "By game":
                    var screenshotByGame = screenshotViewModel.Screenshots.OrderBy(o => o.TitleName).ThenByDescending(x => x.DatePublished).ToList();
                    screenshotViewModel.Screenshots = screenshotByGame;
                    break;

                case "By likes":
                    var screenshotByLikes = screenshotViewModel.Screenshots.OrderByDescending(o => o.RatingCount).ToList();
                    screenshotViewModel.Screenshots = screenshotByLikes;
                    break;

                case "By views":
                    var screenshotByViews = screenshotViewModel.Screenshots.OrderByDescending(o => o.Views).ToList();
                    screenshotViewModel.Screenshots = screenshotByViews;
                    break;
            }
        }
    }
}