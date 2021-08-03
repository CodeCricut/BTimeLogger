using BTimeLogger.Wpf.ViewModels;
using HackerNews.WPF.Core.View;
using System.Windows;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for ModifyIntervalWindow.xaml
	/// </summary>
	public partial class ModifyIntervalWindow : Window, IHaveViewModel<ModifyIntervalWindowViewModel>
	{
		public ModifyIntervalWindowViewModel ViewModel { get; set; }

		public ModifyIntervalWindow()
		{
			InitializeComponent();
		}

		public void SetViewModel(ModifyIntervalWindowViewModel viewModel)
		{
			ViewModel = viewModel;
		}
	}
}
