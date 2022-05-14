using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls;

/// <summary>
/// Interaction logic for MainLayout.xaml
/// </summary>
public partial class MainLayout : UserControl
{
	public MainLayoutViewModel ViewModel
	{
		get { return (MainLayoutViewModel)GetValue(ViewModelProperty); }
		set { SetValue(ViewModelProperty, value); }
	}
	public static readonly DependencyProperty ViewModelProperty =
		DependencyProperty.Register("ViewModel", typeof(MainLayoutViewModel), typeof(MainLayout));

	public MainLayout()
	{
		InitializeComponent();
	}

}
