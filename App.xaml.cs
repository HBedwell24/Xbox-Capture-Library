using MahApps.Metro;
using System;
using System.Windows;

namespace XboxGameClipLibrary
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Add accents for both light and dark application themes
            ThemeManager.AddAccent("DarkAccent", new Uri("pack://application:,,,/XboxGameClipLibrary;component/Resources/Themes/DarkAccent.xaml"));
            ThemeManager.AddAccent("LightAccent", new Uri("pack://application:,,,/XboxGameClipLibrary;component/Resources/Themes/LightAccent.xaml"));

            // If application theme is set to dark theme, apply dark accent
            if (XboxGameClipLibrary.Properties.Settings.Default.appTheme.Equals("darkTheme"))
            {
                ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("DarkAccent"), ThemeManager.GetAppTheme("BaseDark"));
            }
            // Else if application theme is set to light theme, apply light accent
            else if(XboxGameClipLibrary.Properties.Settings.Default.appTheme.Equals("lightTheme"))
            {
                ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("LightAccent"), ThemeManager.GetAppTheme("BaseLight"));
            }            
            
            base.OnStartup(e);
        }
    }
}
