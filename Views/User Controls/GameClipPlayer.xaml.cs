using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace XboxGameClipLibrary.Views.User_Controls
{
	public partial class GameClipPlayer : UserControl
	{
		private bool mediaPlayerIsPlaying = false;
		private bool userIsDraggingSlider = false;

		public GameClipPlayer()
		{
			InitializeComponent();
			
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
		}

		public static readonly DependencyProperty GameClipUriProperty =
		DependencyProperty.Register("GameClipUri", typeof(string), typeof(GameClipPlayer), new PropertyMetadata("NULL"));

		public string GameClipUri
		{
			get { return (string) GetValue(GameClipUriProperty); }
			set { SetValue(GameClipUriProperty, value); }
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
			{
				sliProgress.Minimum = 0;
				sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
				sliProgress.Value = mediaPlayer.Position.TotalSeconds;
			}
		}

		private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
		}

		private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			mediaPlayer.Play();
			mediaPlayerIsPlaying = true;
		}

		private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = mediaPlayerIsPlaying;
		}

		private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			mediaPlayer.Pause();
		}

		private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = mediaPlayerIsPlaying;
		}

		private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			mediaPlayer.Stop();
			mediaPlayerIsPlaying = false;
		}

		private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
		{
			userIsDraggingSlider = true;
		}

		private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			userIsDraggingSlider = false;
			mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
		}

		private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
		}

		private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
		}
	}
}
