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
        DependencyProperty.Register("GameClipId", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipId
        {
            get { return (string)GetValue(GameClipIdProperty); }
            set { SetValue(GameClipIdProperty, value); }
        }

        public static readonly DependencyProperty GameClipUriProperty =
        DependencyProperty.Register("GameClipUri", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipUri
        {
            get { return (string)GetValue(GameClipUriProperty); }
            set { SetValue(GameClipUriProperty, value); }
        }

        public static readonly DependencyProperty GameClipGameProperty =
        DependencyProperty.Register("GameClipGame", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipGame
        {
            get { return (string)GetValue(GameClipGameProperty); }
            set { SetValue(GameClipGameProperty, value); }
        }

        public static readonly DependencyProperty GameClipDeviceProperty =
        DependencyProperty.Register("GameClipDevice", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipDevice
        {
            get { return (string)GetValue(GameClipDeviceProperty); }
            set { SetValue(GameClipDeviceProperty, value); }
        }

        public static readonly DependencyProperty DateRecordedProperty =
        DependencyProperty.Register("DateRecorded", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string DateRecorded
        {
            get { return (string)GetValue(DateRecordedProperty); }
            set { SetValue(DateRecordedProperty, value); }
        }

        public static readonly DependencyProperty GameClipDatePublishedProperty =
        DependencyProperty.Register("GameClipDatePublished", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipDatePublished
        {
            get { return (string)GetValue(GameClipDatePublishedProperty); }
            set { SetValue(GameClipDatePublishedProperty, value); }
        }

        public static readonly DependencyProperty GameClipViewsProperty =
        DependencyProperty.Register("GameClipViews", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipViews
        {
            get { return (string)GetValue(GameClipViewsProperty); }
            set { SetValue(GameClipViewsProperty, value); }
        }

        public static readonly DependencyProperty GameClipLikesProperty =
        DependencyProperty.Register("GameClipLikes", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string GameClipLikes
        {
            get { return (string)GetValue(GameClipLikesProperty); }
            set { SetValue(GameClipLikesProperty, value); }
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
