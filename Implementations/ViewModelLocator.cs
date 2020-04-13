using XboxGameClipLibrary.View_Models;

namespace XboxGameClipLibrary.Implementations
{
    public class ViewModelLocator
    {
        private static readonly DownloadViewModel downloadViewModel;

        static ViewModelLocator()
        {
            downloadViewModel = new DownloadViewModel();
        }

        public static DownloadViewModel DownloadViewModel => downloadViewModel;
    }
}
