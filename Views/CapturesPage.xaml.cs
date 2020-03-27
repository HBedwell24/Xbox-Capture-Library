using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models;
using XboxGameClipLibrary.Models.Screenshots;
using XboxGameClipLibrary.ViewModels.CapturesViewModel;

namespace XboxGameClipLibrary.Views
{
    public partial class CapturesPage : Page
    {
        private bool handle = true;
        List<string> gameClipUriList = new List<string>();
        List<string> thumbnails = new List<string>();

        public CapturesPage()
        {
            // Initialize the CapturesPage component
            InitializeComponent();

            // Bind the capture data to gameClipListView
            Loaded += CapturesPage_Loaded;
        }

        private void CapturesPage_Loaded(object sender, RoutedEventArgs e)
        {
            BindCaptureDataToDataGrid();
            Loaded -= CapturesPage_Loaded;
        }

        private async void BindCaptureDataToDataGrid()
        {
            // Create a CancellationTokenSource object
            CancellationTokenSource cts = new CancellationTokenSource();

            // Display the progress ring
            progressRing.IsActive = true;
            progressRing.Visibility = Visibility.Visible;

            var gameClipData = await GetGameClips(cts.Token);

            foreach (var GameClip in gameClipData)
            {
                gameClipUriList.Add(GameClip.GameClipUris[0].Uri);
                thumbnails.Add(GameClip.Thumbnails[0].Uri);
            }

            // Bind the GameClip and Screenshot data to the view model
            var cvm = new CapturesViewModel
            {
                GameClipUris = gameClipUriList,
                GameClipThumbnails = thumbnails,
                GameClips = gameClipData,
                Screenshots = await GetScreenshots(cts.Token)
            };

            // Request cancellation
            cts.Cancel();

            // Cancellation should have happened, so call Dispose
            cts.Dispose();

            // The data bind has finished, so the ring can now be collapsed
            progressRing.IsActive = false;
            progressRing.Visibility = Visibility.Collapsed;

            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            gameClipListView.ItemsSource = cvm.GameClips;
            screenshotListView.ItemsSource = cvm.Screenshots;

            // Display the correct type of capture data with respect to the ComboBox
            Handle();
        }

        private async Task<List<Screenshot>> GetScreenshots(CancellationToken token)
        {
            var xuid = await GetXuid(token);
            var screenshots = await XboxApiService.GetScreenshotsFromStreamCallAsync(token, xuid);

            // Debug Screenshot response
            string jsonString = JsonConvert.SerializeObject(screenshots, Formatting.Indented);
            Console.WriteLine(jsonString);

            return screenshots;
        }

        private async Task<List<GameClip>> GetGameClips(CancellationToken token)
        {
            var xuid = await GetXuid(token);
            var gameClips = await XboxApiService.GetGameClipsFromStreamCallAsync(token, xuid);

            // Debug GameClip response
            string jsonString = JsonConvert.SerializeObject(gameClips, Formatting.Indented);
            Console.WriteLine(jsonString);

            return gameClips;
        }

        private async Task<string> GetXuid(CancellationToken token)
        {
            var profile = await XboxApiService.GetProfileFromStringCallAsync(token);
            var xuid = profile["userXuid"].ToString();

            // Debug Profile response
            Console.WriteLine(profile);

            // Debug Xuid response
            Console.WriteLine("Xuid: " + xuid);

            return xuid;
        }

        // Click event handler for data grid rows
        private void Row_Click(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            var row = ItemsControl.ContainerFromElement((DataGrid) sender, e.OriginalSource as DependencyObject) as DataGridRow;

            // If empty space was clicked exit method
            if (row == null)
            {
                e.Handled = true;
                return;
            }            

            // TODO: Open URI in MediaPlayer
            Console.WriteLine("The index of the row for the clicked cell is " + FindRowIndex(row));
        }

        // Finds the row index corresponding to the data grid row clicked
        private int FindRowIndex(DataGridRow row)
        {
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;
            int index = dataGrid.ItemContainerGenerator.IndexFromContainer(row);

            return index;
        }

        private void CaptureTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void CaptureTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        // Gets input from comboboxes
        private void Handle()
        {
            switch (captureBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Screenshots":
                    gameClipListView.Visibility = Visibility.Collapsed;
                    screenshotListView.Visibility = Visibility.Visible;
                    break;

                case "Game clips":
                    screenshotListView.Visibility = Visibility.Collapsed;
                    gameClipListView.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}