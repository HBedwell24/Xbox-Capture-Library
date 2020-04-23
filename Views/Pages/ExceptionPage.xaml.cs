using System.Windows.Controls;

namespace XboxGameClipLibrary.Views.Pages
{
    public partial class ExceptionPage : Page
    {
        public ExceptionPage(int statusCode, string content)
        {
            InitializeComponent();
            StatusCode.Content = statusCode;
            Content.Content = content;
        }
    }
}
