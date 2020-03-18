using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XboxGameClipLibrary.Models;

namespace XboxGameClipLibrary.Views
{
    public partial class CapturesPage : Page, IProvideCustomContentState
    {
        public CapturesPage(Task<List<GameClip>> gameClips)
        {
            Console.WriteLine(gameClips.ToString());
            DataContext = gameClips;
        }

        public CustomContentState GetContentState()
        {
            return new RestoreModelContentState(DataContext);
        }
    }

    [Serializable]
    internal class RestoreModelContentState : CustomContentState
    {
        private readonly object _model;

        public RestoreModelContentState(object model)
        {
            _model = model;
        }

        public override void Replay(NavigationService navigationService, NavigationMode mode)
        {
            var element = navigationService.Content as FrameworkElement;
            if (element == null) return;
            element.DataContext = _model;
        }
    }
}