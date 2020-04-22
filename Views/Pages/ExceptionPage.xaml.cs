using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XboxGameClipLibrary.Views.Pages
{
    /// <summary>
    /// Interaction logic for ExceptionPage.xaml
    /// </summary>
    public partial class ExceptionPage : Page
    {
        public ExceptionPage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StatusCodeProperty =
        DependencyProperty.Register("StatusCode", typeof(int), typeof(ExceptionPage));

        public int StatusCode
        {
            get { return (int) GetValue(StatusCodeProperty); }
            set { SetValue(StatusCodeProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(string), typeof(ExceptionPage));

        public string Content
        {
            get { return (string) GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }
}
