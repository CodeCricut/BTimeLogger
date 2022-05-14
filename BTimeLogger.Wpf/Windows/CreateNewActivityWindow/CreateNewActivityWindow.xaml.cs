using System.Windows;

namespace BTimeLogger.Wpf.Windows;

/// <summary>
/// Interaction logic for CreateNewActivityWindow.xaml
/// </summary>
public partial class CreateNewActivityWindow : Window, IHaveViewModel<CreateNewActivityWindowViewModel>
{
	public CreateNewActivityWindowViewModel ViewModel { get; set; }

	public CreateNewActivityWindow()
	{
		InitializeComponent();
	}

	public void SetViewModel(CreateNewActivityWindowViewModel viewModel)
	{
		ViewModel = viewModel;
	}
}
