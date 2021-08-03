using HackerNews.WPF.Core.View;
using System.Windows;

namespace BTimeLogger.Wpf.Windows
{
	/// <summary>
	/// Interaction logic for CreateNewIntervalWindow.xaml
	/// </summary>
	public partial class CreateNewIntervalWindow : Window, IHaveViewModel<CreateNewIntervalWindowViewModel>
	{
		public CreateNewIntervalWindowViewModel ViewModel { get; set; }

		public CreateNewIntervalWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(CreateNewIntervalWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
