using BTimeLogger.Wpf.ViewModels;
using HackerNews.WPF.Core.View;
using System.Windows;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for CreateReportWindow.xaml
	/// </summary>
	public partial class CreateReportWindow : Window, IHaveViewModel<CreateReportWindowViewModel>
	{
		public CreateReportWindowViewModel ViewModel { get; set; }

		public CreateReportWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(CreateReportWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
