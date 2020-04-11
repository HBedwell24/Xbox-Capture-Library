using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace XboxGameClipLibrary
{
    public partial class SplashScreen : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private readonly string credential = Properties.Settings.Default.xboxApiKey;

        public SplashScreen()
        {
            InitializeComponent();

            //if (Debugger.IsAttached)
            //{
            //    Properties.Settings.Default.xboxApiKey = "";
            //    Properties.Settings.Default.Save();
            //}

            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(credential))
            {
                CredentialWindow credentialWindow = new CredentialWindow();
                credentialWindow.Show();
                dispatcherTimer.Stop();
                Close();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                dispatcherTimer.Stop();
                Close();
            }
        }
    }
}
