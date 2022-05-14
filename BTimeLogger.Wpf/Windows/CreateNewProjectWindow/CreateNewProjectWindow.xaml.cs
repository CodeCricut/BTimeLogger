using System.Windows;

namespace BTimeLogger.Wpf.Windows;

/// <summary>
/// Interaction logic for CreateNewProjectWindow.xaml
/// </summary>
public partial class CreateNewProjectWindow : Window, IHaveViewModel<CreateNewProjectWindowViewModel>
{
	public CreateNewProjectWindowViewModel ViewModel { get; private set; }

	public CreateNewProjectWindow()
	{
		InitializeComponent();
	}

	public void SetViewModel(CreateNewProjectWindowViewModel viewModel)
	{
		ViewModel = viewModel;
	}
}
