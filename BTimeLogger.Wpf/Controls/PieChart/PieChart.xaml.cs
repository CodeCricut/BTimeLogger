using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for PieChart.xaml
	/// </summary>
	public partial class PieChart : UserControl
	{
		public PieChartViewModel ViewModel
		{
			get { return (PieChartViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(PieChartViewModel), typeof(PieChart), new PropertyMetadata(null));

		public PieChart()
		{
			InitializeComponent();
		}
	}
}
