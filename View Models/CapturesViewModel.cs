using System.Collections.Generic;
using System.ComponentModel;
using XboxGameClipLibrary.Models;
using XboxGameClipLibrary.Models.Screenshots;

namespace XboxGameClipLibrary.ViewModels.CapturesViewModel
{
    public class CapturesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<GameClip> _GameClips;
        private List<Screenshot> _Screenshots;

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
