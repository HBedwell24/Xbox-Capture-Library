using System.Collections.Generic;
using System.ComponentModel;
using XboxGameClipLibrary.Models;
using XboxGameClipLibrary.Models.Screenshots;

namespace XboxGameClipLibrary.ViewModels.CapturesViewModel
{
    public class CapturesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> _GameClipUris;
        private List<string> _GameClipThumbnails;
        private List<GameClip> _GameClips;
        private List<Screenshot> _Screenshots;

        public List<string> GameClipUris
        {
            get { return _GameClipUris; }
            set
            {
                if (value != _GameClipUris)
                {
                    _GameClipUris = value;
                    OnNotifyPropertyChanged("GameClipUris");
                }
            }
        }

        public List<string> GameClipThumbnails
        {
            get { return _GameClipThumbnails; }
            set
            {
                if (value != _GameClipThumbnails)
                {
                    _GameClipThumbnails = value;
                    OnNotifyPropertyChanged("GameClipThumbnails");
                }
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
