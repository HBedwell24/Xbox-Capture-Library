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

            // Create List objects to store the data in
            List<GameClip> gameClips = await XboxApiImpl.GetGameClips(cts.Token);
            List<Screenshot> screenshots = await XboxApiImpl.GetScreenshots(cts.Token);

            // Request cancellation
            cts.Cancel();

            // Cancellation should have happened, so call Dispose
            cts.Dispose();

            // Set the data in the respective property
            GameClips = gameClips;
            Screenshots = screenshots;
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
