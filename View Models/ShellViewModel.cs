﻿using System;
using System.Linq;
using MahApps.Metro.IconPacks;

namespace XboxGameClipLibrary.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            Menu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CameraSolid }, Text = "Captures", NavigationDestination = new Uri("Views/Pages/CapturesPage.xaml", UriKind.RelativeOrAbsolute) });
            OptionsMenu.Add(new MenuItemViewModel() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogsSolid }, Text = "Settings", NavigationDestination = new Uri("Views/Windows/SettingsWindow.xaml", UriKind.RelativeOrAbsolute) });
        }

        public object GetItem(object uri)
        {
            return null == uri ? null : Menu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }

        public object GetOptionsItem(object uri)
        {
            return null == uri ? null : OptionsMenu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }
    }
}
