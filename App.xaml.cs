using MahApps.Metro;
using System;
using System.Windows;

namespace XboxGameClipLibrary
{
    public partial class App : Application
    {
        readonly string credential = XboxGameClipLibrary.Properties.Settings.Default.xboxApiKey;

        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.AddAccent("Accent", new Uri("pack://application:,,,/XboxGameClipLibrary;component/Resources/Themes/Accent.xaml"));
            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("Accent"), ThemeManager.GetAppTheme("BaseDark"));

            if (string.IsNullOrEmpty(credential))
            {
                EnterUserDetails enterUserDetails = new EnterUserDetails();
                enterUserDetails.Show();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            base.OnStartup(e);
        }
    }
}
