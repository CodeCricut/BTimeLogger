using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls;

/// <summary>
/// Interaction logic for ModifyInterval.xaml
/// </summary>
public partial class ModifyInterval : UserControl
{


	public ModifyIntervalViewModel ViewModel
	{
		get { return (ModifyIntervalViewModel)GetValue(ViewModelProperty); }
		set { SetValue(ViewModelProperty, value); }
	}

	// Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
	public static readonly DependencyProperty ViewModelProperty =
		DependencyProperty.Register("ViewModel", typeof(ModifyIntervalViewModel), typeof(ModifyInterval), new PropertyMetadata(null));

	public ModifyInterval()
	{
		InitializeComponent();
	}
}
