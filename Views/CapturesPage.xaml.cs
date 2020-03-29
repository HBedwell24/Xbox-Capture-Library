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
        private bool handle = true;

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

        private void BindCaptureDataToDataGrid()
        {
            // Bind the Game Clip capture data to the itemssource of the gameClipListView
            DataContext = new CapturesViewModel();

            // Display the correct type of capture data with respect to the ComboBox
            //Handle();
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
            //player.Source = new Uri("");
            //player.Play();
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