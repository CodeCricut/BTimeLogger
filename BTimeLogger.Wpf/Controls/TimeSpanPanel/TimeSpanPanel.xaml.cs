using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls;

/// <summary>
/// Interaction logic for TimeSpanPanel.xaml
/// </summary>
public partial class TimeSpanPanel : UserControl
{
	public TimeSpanPanelViewModel ViewModel
	{
		get { return (TimeSpanPanelViewModel)GetValue(ViewModelProperty); }
		set { SetValue(ViewModelProperty, value); }
	}
	public static readonly DependencyProperty ViewModelProperty =
		DependencyProperty.Register("ViewModel", typeof(TimeSpanPanelViewModel), typeof(TimeSpanPanel), new PropertyMetadata(null));

	public TimeSpanPanel()
	{
		InitializeComponent();
	}
}
