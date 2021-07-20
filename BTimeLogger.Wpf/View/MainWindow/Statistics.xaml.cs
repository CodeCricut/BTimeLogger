using BTimeLogger.Wpf.ViewModels.MainWindow;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View.MainWindow
{
	/// <summary>
	/// Interaction logic for Statistics.xaml
	/// </summary>
	public partial class Statistics : UserControl
	{
		public StatisticsViewModel ViewModel
		{
			get { return (StatisticsViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(StatisticsViewModel), typeof(Statistics));

		public Statistics()
		{
			InitializeComponent();
		}
	}
}
