using System;
using System.Windows;
using System.Windows.Threading;

namespace XboxCaptureLibrary
{
    public partial class SplashScreen : Window
    {
        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private string credential;

        public SplashScreen()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            credential = Properties.Settings.Default.xboxApiKey;

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
