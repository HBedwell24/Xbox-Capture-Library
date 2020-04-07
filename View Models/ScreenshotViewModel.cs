using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models.Screenshots;

namespace XboxGameClipLibrary.ViewModels.ScreenshotViewModel
{
    public class ScreenshotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _ProgressRingActive;
        private string _ProgressRingVisibility;
        private string _ProgressRingWrapperVisibility;
        private string _ContentWrapperVisibility;

        private List<Screenshot> _Screenshots;

        public ScreenshotViewModel()
        {
            LoadScreenshotData();
        }

        public async void LoadScreenshotData()
        {
            // Display the progress ring
            ContentWrapperVisibility = "Collapsed";
            ProgressRingWrapperVisibility = "Visible";
            ProgressRingActive = "True";
            ProgressRingVisibility = "Visible";

            // Create a CancellationTokenSource object
            CancellationTokenSource cts = new CancellationTokenSource();

            // Bind the capture data
            Screenshots = await XboxApiImpl.GetScreenshots(cts.Token);

            // Request cancellation
            cts.Cancel();

            // Cancellation should have happened, so call Dispose
            cts.Dispose();

            // The data bind has finished, so the ring can now be collapsed
            ProgressRingActive = "False";
            ProgressRingVisibility = "Collapsed";
            ProgressRingWrapperVisibility = "Collapsed";
            ContentWrapperVisibility = "Visible";
        }

        public string ProgressRingActive
        {
            get
            {
                return _ProgressRingActive;
            }

            set
            {
                _ProgressRingActive = value;
                OnNotifyPropertyChanged("ProgressRingActive");
            }
        }

        public string ProgressRingVisibility
        {
            get
            {
                return _ProgressRingVisibility;
            }

            set
            {
                _ProgressRingVisibility = value;
                OnNotifyPropertyChanged("ProgressRingVisibility");
            }
        }

        public string ProgressRingWrapperVisibility
        {
            get
            {
                return _ProgressRingWrapperVisibility;
            }

            set
            {
                _ProgressRingWrapperVisibility = value;
                OnNotifyPropertyChanged("ProgressRingWrapperVisibility");
            }
        }

        public string ContentWrapperVisibility
        {
            get
            {
                return _ContentWrapperVisibility;
            }

            set
            {
                _ContentWrapperVisibility = value;
                OnNotifyPropertyChanged("ContentWrapperVisibility");
            }
        }

        public List<Screenshot> Screenshots
        {
            get { return _Screenshots; }
            set
            {
                if (value != _Screenshots)
                {
                    _Screenshots = value;
                    OnNotifyPropertyChanged("Screenshots");
                }
            }
        }

        private void OnNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
