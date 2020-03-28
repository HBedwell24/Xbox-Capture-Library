using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models;
using XboxGameClipLibrary.Models.Screenshots;

namespace XboxGameClipLibrary.ViewModels.CapturesViewModel
{
    public class CapturesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _ProgressRingActive;
        private string _ProgressRingVisibility;

        private List<GameClip> _GameClips;
        private List<Screenshot> _Screenshots;

        public CapturesViewModel()
        {
            LoadCaptureData();
        }

        public async void LoadCaptureData()
        {
            // Create a CancellationTokenSource object
            CancellationTokenSource cts = new CancellationTokenSource();

            // Display the progress ring
            ProgressRingActive = "True";
            ProgressRingVisibility = "Visible";

            // Bind the capture data
            GameClips = await XboxApiImpl.GetGameClips(cts.Token);
            Screenshots = await XboxApiImpl.GetScreenshots(cts.Token);

            // Request cancellation
            cts.Cancel();

            // Cancellation should have happened, so call Dispose
            cts.Dispose();

            // The data bind has finished, so the ring can now be collapsed
            ProgressRingActive = "False";
            ProgressRingVisibility = "Collapsed";
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

        public List<GameClip> GameClips
        {
            get { return _GameClips; }
            set
            {
                if (value != _GameClips)
                {
                    _GameClips = value;
                    OnNotifyPropertyChanged("GameClips");
                }
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
