using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for TitleBarMenu.xaml
	/// </summary>
	public partial class TitleBarMenu : UserControl
	{
		public TitleBarMenuViewModel ViewModel
		{
			get { return (TitleBarMenuViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(TitleBarMenuViewModel), typeof(TitleBarMenu), new PropertyMetadata(null));

		public TitleBarMenu()
		{
			InitializeComponent();
		}
	}
}
