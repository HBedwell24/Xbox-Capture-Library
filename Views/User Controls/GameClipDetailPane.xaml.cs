using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace XboxGameClipLibrary.Views
{
    public partial class GameClipDetailPane : UserControl
    {
        public GameClipDetailPane()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty GameClipIdProperty =
        DependencyProperty.Register("GameClipId", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string GameClipId
        {
            get { return (string) GetValue(GameClipIdProperty); }
            set { SetValue(GameClipIdProperty, value); }
        }

        public static readonly DependencyProperty GameClipUriProperty =
        DependencyProperty.Register("GameClipUri", typeof(string), typeof(GameClipDetailPane), new PropertyMetadata("NULL"));

        public string GameClipUri
        {
            get { return (string) GetValue(GameClipUriProperty); }
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
                client.DownloadFileAsync(new Uri(GameClipUri), downloadPath + GameClipId + ".png");
            }
        }

        public void Share_Screenshot(object sender, RoutedEventArgs e)
        {

        }
    }
}
