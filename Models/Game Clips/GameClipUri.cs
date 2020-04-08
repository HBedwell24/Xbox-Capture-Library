using System;

namespace XboxGameClipLibrary.Models
{
    public class GameClipUri
    {
        private string _Uri;

        public Uri Uri { 
            get
            {
                var httpGameClipUri = _Uri.Remove(4, 1);
                return new Uri(httpGameClipUri);
            }
            set
            {
                _Uri = value.ToString();
            }
        }
        public int FileSize { get; set; }
        public string UriType { get; set; }
        public string Expiration { get; set; }
    }
}
