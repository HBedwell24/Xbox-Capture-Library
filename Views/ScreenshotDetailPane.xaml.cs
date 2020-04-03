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
        DependencyProperty.Register("ScreenshotId", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string ScreenshotId
        {
            get { return (string)GetValue(ScreenshotIdProperty); }
            set { SetValue(ScreenshotIdProperty, value); }
        }

        public static readonly DependencyProperty ScreenshotUriProperty =
        DependencyProperty.Register("ScreenshotUri", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string ScreenshotUri
        {
            get { return (string)GetValue(ScreenshotUriProperty); }
            set { SetValue(ScreenshotUriProperty, value); }
        }

        public static readonly DependencyProperty GameProperty =
        DependencyProperty.Register("Game", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string Game
        {
            get { return (string)GetValue(GameProperty); }
            set { SetValue(GameProperty, value); }
        }

        public static readonly DependencyProperty DeviceProperty =
        DependencyProperty.Register("Device", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string Device
        {
            get { return (string)GetValue(DeviceProperty); }
            set { SetValue(DeviceProperty, value); }
        }

        public static readonly DependencyProperty DateTakenProperty =
        DependencyProperty.Register("DateTaken", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string DateTaken
        {
            get { return (string)GetValue(DateTakenProperty); }
            set { SetValue(DateTakenProperty, value); }
        }

        public static readonly DependencyProperty DatePublishedProperty =
        DependencyProperty.Register("DatePublished", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string DatePublished
        {
            get { return (string)GetValue(DatePublishedProperty); }
            set { SetValue(DatePublishedProperty, value); }
        }

        public static readonly DependencyProperty ViewsProperty =
        DependencyProperty.Register("Views", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string Views
        {
            get { return (string)GetValue(ViewsProperty); }
            set { SetValue(ViewsProperty, value); }
        }

        public static readonly DependencyProperty LikesProperty =
        DependencyProperty.Register("Likes", typeof(string), typeof(ScreenshotDetailPane), new PropertyMetadata("default value"));

        public string Likes
        {
            get { return (string)GetValue(LikesProperty); }
            set { SetValue(LikesProperty, value); }
        }

        public void Download_Image_Content(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(ScreenshotUri), @"c:\Downloads\" + ScreenshotId + ".png");
            }
        }
    }
}
