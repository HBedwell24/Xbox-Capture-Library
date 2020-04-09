using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace XboxGameClipLibrary.Views
{
    public partial class GameClipDetailPane : UserControl
    {
        private bool MediaPlayerIsPlaying = false;
        private bool UserIsDraggingSlider = false;
        private bool FilterHandle = true;

        public GameClipDetailPane()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public static readonly DependencyProperty GameClipIdProperty =
        DependencyProperty.Register("GameClipId", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string GameClipId
        {
            get { return (string) GetValue(GameClipIdProperty); }
            set { SetValue(GameClipIdProperty, value); }
        }

        public static readonly DependencyProperty GameClipUriProperty =
        DependencyProperty.Register("GameClipUri", typeof(Uri), typeof(GameClipDetailPane));

        public Uri GameClipUri
        {
            get { return (Uri) GetValue(GameClipUriProperty); }
            set { SetValue(GameClipUriProperty, value); }
        }

        public static readonly DependencyProperty GameProperty =
        DependencyProperty.Register("Game", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string Game
        {
            get { return (string) GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty DeviceProperty =
        DependencyProperty.Register("Device", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string Device
        {
            get { return (string) GetValue(DeviceProperty); }
            set { SetValue(DeviceProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
        DependencyProperty.Register("Duration", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string Duration
        {
            get { return (string) GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DateRecordedProperty =
        DependencyProperty.Register("DateRecorded", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string DateRecorded
        {
            get { return (string) GetValue(DateRecordedProperty); }
            set { SetValue(DateRecordedProperty, value); }
        }

        public static readonly DependencyProperty DatePublishedProperty =
        DependencyProperty.Register("DatePublished", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string DatePublished
        {
            get { return (string)GetValue(DatePublishedProperty); }
            set { SetValue(DatePublishedProperty, value); }
        }

        public static readonly DependencyProperty ViewsProperty =
        DependencyProperty.Register("Views", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string Views
        {
            get { return (string)GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        public static readonly DependencyProperty LikesProperty =
        DependencyProperty.Register("Likes", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string Likes
        {
            get { return (string)GetValue(LikesProperty); }
            set { SetValue(LikesProperty, value); }
        }

        void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("Download status: {0}%.", e.ProgressPercentage);
        }

        public void Download_Image_Content(object sender, RoutedEventArgs e)
        {
            string downloadPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Downloads\");

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += WebClientDownloadProgressChanged;
                client.DownloadFileAsync(GameClipUri, downloadPath + GameClipId + ".mp4");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!UserIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Play();
            MediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = MediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            UserIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            UserIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
            timeLeft.Text = TimeSpan.FromSeconds(sliProgress.Maximum - sliProgress.Value).ToString(@"hh\:mm\:ss");
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void volSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = volSlider.Value;
        }

        private void SpeedRatio_DropDownClosed(object sender, EventArgs e)
        {
            if (FilterHandle) HandleSpeedRatio();
            FilterHandle = true;
        }

        private void SpeedRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            FilterHandle = !cmb.IsDropDownOpen;
            HandleSpeedRatio();
        }

        // Gets input from comboboxes
        private void HandleSpeedRatio()
        {
            switch (speedRatioComboBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "x0.25":
                    mediaPlayer.SpeedRatio = 0.25;
                    break;

                case "x0.5":
                    mediaPlayer.SpeedRatio = 0.5;
                    break;

                case "x0.75":
                    mediaPlayer.SpeedRatio = 0.75;
                    break;

                case "Normal":
                    mediaPlayer.SpeedRatio = 1;
                    break;

                case "x1.25":
                    mediaPlayer.SpeedRatio = 1.25;
                    break;

                case "x1.5":
                    mediaPlayer.SpeedRatio = 1.5;
                    break;

                case "x1.75":
                    mediaPlayer.SpeedRatio = 1.75;
                    break;

                case "x2":
                    mediaPlayer.SpeedRatio = 2;
                    break;
            }
        }


    }
}
