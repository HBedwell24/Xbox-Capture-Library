using MahApps.Metro;
using System;
using System.Windows;

namespace XboxGameClipLibrary
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.AddAccent("Accent", new Uri("pack://application:,,,/XboxGameClipLibrary;component/Resources/Themes/Accent.xaml"));
            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("Accent"), ThemeManager.GetAppTheme("BaseDark"));
            base.OnStartup(e);
        }
    }
}
