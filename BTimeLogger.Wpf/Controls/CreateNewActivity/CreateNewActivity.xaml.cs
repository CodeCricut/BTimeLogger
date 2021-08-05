using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for CreateNewActivity.xaml
	/// </summary>
	public partial class CreateNewActivity : UserControl
	{
		public CreateNewActivityViewModel ViewModel
		{
			get { return (CreateNewActivityViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(CreateNewActivityViewModel), typeof(CreateNewActivity), new PropertyMetadata(null));

		public CreateNewActivity()
		{
			InitializeComponent();
		}
	}
}
