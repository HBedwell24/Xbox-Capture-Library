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

            if (XboxGameClipLibrary.Properties.Settings.Default.appTheme.Equals("darkTheme"))
            {
                ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("Accent"), ThemeManager.GetAppTheme("BaseDark"));
            }
            else if(XboxGameClipLibrary.Properties.Settings.Default.appTheme.Equals("lightTheme"))
            {
                ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent("Accent"), ThemeManager.GetAppTheme("BaseLight"));
            }            
            
            base.OnStartup(e);
        }
    }
}
