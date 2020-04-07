using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace XboxGameClipLibrary.Views
{
    public partial class ScreenshotDetailPane : UserControl
    {
        public ScreenshotDetailPane()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ScreenshotIdProperty =
        DependencyProperty.Register("ScreenshotId", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string ScreenshotId
        {
            get { return (string)GetValue(ScreenshotIdProperty); }
            set { SetValue(ScreenshotIdProperty, value); }
        }

        public static readonly DependencyProperty ScreenshotUriProperty =
        DependencyProperty.Register("ScreenshotUri", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string ScreenshotUri
        {
            get { return (string)GetValue(ScreenshotUriProperty); }
            set { SetValue(ScreenshotUriProperty, value); }
        }

        public static readonly DependencyProperty GameProperty =
        DependencyProperty.Register("Game", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string Game
        {
            get { return (string)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty DeviceProperty =
        DependencyProperty.Register("Device", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string Device
        {
            get { return (string)GetValue(DeviceProperty); }
            set { SetValue(DeviceProperty, value); }
        }

        public static readonly DependencyProperty DateTakenProperty =
        DependencyProperty.Register("DateTaken", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string DateTaken
        {
            get { return (string)GetValue(DateTakenProperty); }
            set { SetValue(DateTakenProperty, value); }
        }

        public static readonly DependencyProperty DatePublishedProperty =
        DependencyProperty.Register("DatePublished", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string DatePublished
        {
            get { return (string)GetValue(DatePublishedProperty); }
            set { SetValue(DatePublishedProperty, value); }
        }

        public static readonly DependencyProperty ViewsProperty =
        DependencyProperty.Register("Views", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

        public string Views
        {
            get { return (string)GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        public static readonly DependencyProperty LikesProperty =
        DependencyProperty.Register("Likes", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("NULL"));

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
                client.DownloadFileAsync(new Uri(ScreenshotUri), downloadPath + ScreenshotId + ".png");
            }
        }

        public void Share_Screenshot(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
