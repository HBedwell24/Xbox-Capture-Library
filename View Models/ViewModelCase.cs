using System.Collections.ObjectModel;
using XboxGameClipLibrary.Mvvm;

namespace XboxGameClipLibrary.ViewModels
{
    internal class ViewModelBase : BindableBase
    {
        private static readonly ObservableCollection<MenuItemViewModel> AppMenu = new ObservableCollection<MenuItemViewModel>();
        private static readonly ObservableCollection<MenuItemViewModel> AppOptionsMenu = new ObservableCollection<MenuItemViewModel>();

        public ViewModelBase()
        {
        }

        public ObservableCollection<MenuItemViewModel> Menu => AppMenu;

        public ObservableCollection<MenuItemViewModel> OptionsMenu => AppOptionsMenu;
    }
}
