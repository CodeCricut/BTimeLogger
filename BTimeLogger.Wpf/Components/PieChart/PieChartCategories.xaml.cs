using BTimeLogger.Wpf.ViewModels.PieChart;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for PieChartCategories.xaml
	/// </summary>
	public partial class PieChartCategories : UserControl
	{
		public PieChartViewModel ViewModel
		{
			get { return (PieChartViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(PieChartViewModel), typeof(PieChartCategories), new PropertyMetadata(null));


		public PieChartCategories()
		{
			InitializeComponent();
		}
	}
}
