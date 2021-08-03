using HackerNews.WPF.Core.View;
using System.Windows;

namespace BTimeLogger.Wpf.Windows
{
	/// <summary>
	/// Interaction logic for SaveAsWindow.xaml
	/// </summary>
	public partial class SaveAsWindow : Window, IHaveViewModel<SaveAsWindowViewModel>
	{
		public SaveAsWindowViewModel ViewModel { get; set; }

		public SaveAsWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(SaveAsWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
