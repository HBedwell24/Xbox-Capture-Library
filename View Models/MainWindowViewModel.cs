using System.ComponentModel;
using System.Threading;
using XboxGameClipLibrary.API;

namespace XboxGameClipLibrary.ViewModels.GameClipViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _GamerTag;
        private string _GamerTagVisibility;

        public MainWindowViewModel()
        {
            GetGamerTag();
        }

        public async void GetGamerTag()
        {
            // Create a CancellationTokenSource object
            CancellationTokenSource cts = new CancellationTokenSource();

            GamerTagVisibility = "Collapsed";

            // Bind the capture data
            GamerTag = await XboxApiImpl.GetGamerTag(cts.Token);

            // Request cancellation
            cts.Cancel();

            // Cancellation should have happened, so call Dispose
            cts.Dispose();

            if (GamerTag != null)
            {
                GamerTagVisibility = "Visible";
            }
        }

        public string GamerTag
        {
            get
            {
                return _GamerTag;
            }

            set
            {
                _GamerTag = value;
                OnNotifyPropertyChanged("GamerTag");
            }
        }

        public string GamerTagVisibility
        {
            get
            {
                return _GamerTagVisibility;
            }

            set
            {
                _GamerTagVisibility = value;
                OnNotifyPropertyChanged("GamerTagVisibility");
            }
        }

        private void OnNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

