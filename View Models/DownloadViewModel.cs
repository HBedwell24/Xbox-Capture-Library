using System.ComponentModel;

namespace XboxGameClipLibrary.View_Models
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _CaptureId;
        private string _DownloadProgressInMb;
        private string _TotalMb;
        private string _PercentageCompleted;

        public string CaptureId
        {
            get
            {
                return _CaptureId;
            }

            set
            {
                _CaptureId = value;
                OnNotifyPropertyChanged("CaptureId");
            }
        }

        public string DownloadProgressInMb
        {
            get
            {
                return _DownloadProgressInMb;
            }

            set
            {
                _DownloadProgressInMb = value;
                OnNotifyPropertyChanged("DownloadProgressInMb");
            }
        }

        public string TotalMb
        {
            get
            {
                return _TotalMb;
            }

            set
            {
                _TotalMb = value;
                OnNotifyPropertyChanged("TotalMb");
            }
        }

        public string PercentageCompleted
        {
            get
            {
                return _PercentageCompleted;
            }

            set
            {
                _PercentageCompleted = value;
                OnNotifyPropertyChanged("PercentageCompleted");
            }
        }

        private void OnNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
