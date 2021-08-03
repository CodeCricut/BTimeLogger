using BTimeLogger.Wpf.ViewModels.MainWindow;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View.MainWindow
{
	/// <summary>
	/// Interaction logic for Intervals.xaml
	/// </summary>
	public partial class Intervals : UserControl
	{
		public IntervalsViewModel ViewModel
		{
			get { return (IntervalsViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(IntervalsViewModel), typeof(Intervals));

		public Intervals()
		{
			InitializeComponent();
		}
	}
}
