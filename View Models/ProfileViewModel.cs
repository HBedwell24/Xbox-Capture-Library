using System.ComponentModel;

namespace XboxGameClipLibrary.Models.Profile
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string userName;
        private string profilePicUri;

        public string UserName 
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string ProfilePicUri {
            get
            {
                return profilePicUri;
            }
            set
            {
                profilePicUri = value;
                OnPropertyChanged("ProfilePicUri");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
