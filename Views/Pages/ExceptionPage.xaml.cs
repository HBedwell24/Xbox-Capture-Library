using System.Windows.Controls;

namespace XboxCaptureLibrary.Views.Pages
{
    public partial class ExceptionPage : Page
    {
        public ExceptionPage(int statusCode, string reasonPhrase, string errorMessage)
        {
            InitializeComponent();
            StatusCode.Text = statusCode.ToString();
            ReasonPhrase.Text = reasonPhrase;
            ErrorMessage.Text = errorMessage + ".";

        }
    }
}
