using System.Collections.Generic;
using System.ComponentModel;
using XboxGameClipLibrary.Models;

namespace XboxGameClipLibrary.ViewModels.CapturesPage
{
    public class GameClipViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<GameClip> _GameClips;
        private List<GameClip> GameClips
        {
            get { return _GameClips; }
            set
            {
                if (value != _GameClips)
                {
                    _GameClips = value;
                    OnNotifyPropertyChanged(nameof(GameClips));
                }
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
