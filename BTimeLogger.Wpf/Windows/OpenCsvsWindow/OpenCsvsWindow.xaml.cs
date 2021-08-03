using HackerNews.WPF.Core.View;
using System.Windows;

namespace BTimeLogger.Wpf.Windows
{
	/// <summary>
	/// Interaction logic for CreateReportWindow.xaml
	/// </summary>
	public partial class OpenCsvsWindow : Window, IHaveViewModel<OpenCsvsWindowViewModel>
	{
		public OpenCsvsWindowViewModel ViewModel { get; set; }

		public OpenCsvsWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(OpenCsvsWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
