using System.Collections.Generic;
using System.ComponentModel;
using XboxGameClipLibrary.Models.Screenshots;

namespace XboxGameClipLibrary.ViewModels
{
    public class ScreenshotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Screenshot> _Screenshot;
        private List<Screenshot> Screenshot
        {
            get { return _Screenshot; }
            set
            {
                if (value != _Screenshot)
                {
                    _Screenshot = value;
                    OnNotifyPropertyChanged(nameof(Screenshot));
                }
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
