using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using XboxGameClipLibrary.Models;

namespace XboxGameClipLibrary.ViewModels.CapturesPage
{
    public class CapturesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<GameClip> _GameClips;
        private JObject _Screenshots;

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

        public JObject Screenshots
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
