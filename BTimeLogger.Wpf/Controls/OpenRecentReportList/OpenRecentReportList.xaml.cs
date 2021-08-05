using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for OpenRecentReportList.xaml
	/// </summary>
	public partial class OpenRecentReportList : UserControl
	{
		public OpenRecentReportListViewModel ViewModel
		{
			get { return (OpenRecentReportListViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(OpenRecentReportListViewModel), typeof(OpenRecentReportList), new PropertyMetadata(null));

		public OpenRecentReportList()
		{
			InitializeComponent();
		}
	}
}
