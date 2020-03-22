using MahApps.Metro;
using System;
using System.Windows;

namespace XboxGameClipLibrary
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // add custom accent and theme resource dictionaries to the ThemeManager
            // you should replace MahAppsMetroThemesSample with your application name
            // and correct place where your custom accent lives
            ThemeManager.AddAccent("Accent", new Uri("pack://application:,,,/XboxGameClipLibrary;component/Resources/Themes/Accent.xaml"));

            // get the current app style (theme and accent) from the application
            // Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Current);

            // now change app style to the custom accent and current theme
            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("Accent"), ThemeManager.GetAppTheme("BaseDark"));

            base.OnStartup(e);
        }
    }
}
