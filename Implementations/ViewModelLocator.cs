using XboxGameClipLibrary.View_Models;

namespace XboxGameClipLibrary.Implementations
{
    public class ViewModelLocator
    {
        private DownloadViewModel _DownloadViewModel;
        public DownloadViewModel DownloadViewModel
        {
            get
            {
                if (_DownloadViewModel == null)
                    _DownloadViewModel = new DownloadViewModel();

                return _DownloadViewModel;
            }
        }
    }
}
