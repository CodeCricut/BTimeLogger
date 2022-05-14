using BTimeLogger.Wpf.Util;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls;

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

	public bool DisableCloseInteraction
	{
		get { return (bool)GetValue(DisableCloseInteractionProperty); }
		set { SetValue(DisableCloseInteractionProperty, value); }
	}
	public static readonly DependencyProperty DisableCloseInteractionProperty =
		DependencyProperty.Register("DisableCloseInteraction", typeof(bool), typeof(WindowButtons), new PropertyMetadata(false));

	public WindowButtons()
	{
		InitializeComponent();
	}

	private void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		if (ViewModel == null) return;

		ViewModel.Minimized += ViewModel_Minimized;
		ViewModel.Restored += ViewModel_Restored;
		ViewModel.Maximized += ViewModel_Maximized;
		ViewModel.Closed += ViewModel_Closed;
	}

	private void ViewModel_Minimized(object sender, EventArgs e)
	{
		UpdateWindowState(WindowState.Minimized);
	}

	private void ViewModel_Restored(object sender, EventArgs e)
	{
		UpdateWindowState(WindowState.Normal);
	}

	private void ViewModel_Maximized(object sender, EventArgs e)
	{
		UpdateWindowState(WindowState.Maximized);
	}

	private void ViewModel_Closed(object sender, EventArgs e)
	{
		if (DisableCloseInteraction) return;

		var windowParent = VisualTreeUtil.FindParent<Window>(this);
		if (windowParent != null)
		{
			windowParent.Close();
		}
	}

	private void UpdateWindowState(WindowState newState)
	{
		var windowParent = VisualTreeUtil.FindParent<Window>(this);

		if (windowParent != null)
		{
			windowParent.WindowState = newState;
		}
	}
}
