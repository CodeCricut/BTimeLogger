using BTimeLogger.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for CreateNewInterval.xaml
	/// </summary>
	public partial class CreateNewInterval : UserControl
	{
		public CreateNewIntervalViewModel ViewModel
		{
			get { return (CreateNewIntervalViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(CreateNewIntervalViewModel), typeof(CreateNewInterval), new PropertyMetadata(null));

		public CreateNewInterval()
		{
			InitializeComponent();
		}
	}
}
