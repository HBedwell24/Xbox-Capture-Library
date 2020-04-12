using DevExpress.Mvvm.DataAnnotations;

namespace XboxGameClipLibrary.View_Models
{
    [POCOViewModel]
    public class CustomNotificationViewModel
    {
        public virtual string Caption { get; set; }
        public virtual string Content { get; set; }
    }
}
