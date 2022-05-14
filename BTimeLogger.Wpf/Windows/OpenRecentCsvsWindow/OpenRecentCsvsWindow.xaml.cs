using System.Windows;

namespace BTimeLogger.Wpf.Windows;

/// <summary>
/// Interaction logic for OpenRecentCsvsWindow.xaml
/// </summary>
public partial class OpenRecentCsvsWindow : Window, IHaveViewModel<OpenRecentCsvsWindowViewModel>
{
	public OpenRecentCsvsWindowViewModel ViewModel { get; private set; }

	public OpenRecentCsvsWindow()
	{
		InitializeComponent();
	}

	public void SetViewModel(OpenRecentCsvsWindowViewModel viewModel)
	{
		ViewModel = viewModel;
	}
}
