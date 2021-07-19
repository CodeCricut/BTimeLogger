using BTimeLogger.Wpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for WindowButtons.xaml
	/// </summary>
	public partial class WindowButtons : UserControl
	{
		public WindowButtonsViewModel ViewModel
		{
			get { return (WindowButtonsViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(WindowButtonsViewModel), typeof(WindowButtons));

		public WindowButtons()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			if (ViewModel != null)
			{
				ViewModel.Minimized += ViewModel_Minimized;
				ViewModel.Restored += ViewModel_Restored;
				ViewModel.Maximized += ViewModel_Maximized;
				ViewModel.Closed += ViewModel_Closed;
			}
		}

		private void ViewModel_Minimized(object sender, System.EventArgs e)
		{
		}

		private void ViewModel_Restored(object sender, System.EventArgs e)
		{
		}

		private void ViewModel_Maximized(object sender, System.EventArgs e)
		{
		}

		private void ViewModel_Closed(object sender, EventArgs e)
		{
		}
	}
}
