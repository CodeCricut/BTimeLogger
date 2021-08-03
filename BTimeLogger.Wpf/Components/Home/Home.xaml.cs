using BTimeLogger.Wpf.ViewModels.MainWindow;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View.MainWindow
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
