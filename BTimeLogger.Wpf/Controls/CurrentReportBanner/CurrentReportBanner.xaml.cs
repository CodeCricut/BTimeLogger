using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for CurrentReportBanner.xaml
	/// </summary>
	public partial class CurrentReportBanner : UserControl
	{
		public CurrentReportBannerViewModel ViewModel
		{
			get { return (CurrentReportBannerViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(CurrentReportBannerViewModel), typeof(CurrentReportBanner), new PropertyMetadata(null));

		public CurrentReportBanner()
		{
			InitializeComponent();
		}
	}
}
