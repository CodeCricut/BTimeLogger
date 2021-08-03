using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : UserControl
	{
		public HomeViewModel ViewModel
		{
			get { return (HomeViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(HomeViewModel), typeof(Home));

		public Home()
		{
			InitializeComponent();
		}
	}
}
