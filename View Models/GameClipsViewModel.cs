using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using XboxGameClipLibrary.API;
using XboxGameClipLibrary.Models;

namespace XboxGameClipLibrary.ViewModels.GameClipsViewModel
{
    public class GameClipsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _ProgressRingActive;
        private string _ProgressRingVisibility;
        private string _ProgressRingWrapperVisibility;
        private string _ContentWrapperVisibility;

        private List<GameClip> _GameClips;

        public GameClipsViewModel()
        {
            LoadGameClipData();
        }

        public async void LoadGameClipData()
        {
            // Display the progress ring
            ContentWrapperVisibility = "Collapsed";
            ProgressRingWrapperVisibility = "Visible";
            ProgressRingActive = "True";
            ProgressRingVisibility = "Visible";

            // Create a CancellationTokenSource object
            CancellationTokenSource cts = new CancellationTokenSource();

            // Bind the capture data
            GameClips = await XboxApiImpl.GetGameClips(cts.Token);

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

        private void OnNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

