using BTimeLogger.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View
{
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

		private void Layout_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel.SelectHomeCommand.Execute();
		}
	}
}
