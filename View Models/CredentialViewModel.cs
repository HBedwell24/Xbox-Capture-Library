using XboxGameClipLibrary.Extensions;

namespace XboxGameClipLibrary.View_Models
{
    public class CredentialViewModel : ObservableObject
    {
        private string _Credential;
        public string Credential
        {
            get { return _Credential; }
            set
            {
                OnPropertyChanged(ref _Credential, value);
            }
        }
    }
}
