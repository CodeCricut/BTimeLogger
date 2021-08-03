using HackerNews.WPF.Core.View;
using System.Windows;

namespace BTimeLogger.Wpf.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IHaveViewModel<MainWindowViewModel>
	{
		public MainWindowViewModel ViewModel { get; set; }
		public MainWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(MainWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
