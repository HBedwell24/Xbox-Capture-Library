using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MahApps.Metro.Controls;

namespace XboxGameClipLibrary.ViewModels
{
    internal class MenuItemViewModel : HamburgerMenuItem, INotifyPropertyChanged
    {
        private object _icon;
        private string _text;
        private object _navigationDestination;

        public object Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public object NavigationDestination
        {
            get { return _navigationDestination; }
            set { SetProperty(ref _navigationDestination, value); }
        }

        public bool IsNavigation => _navigationDestination != null;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
