using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for GroupedActivityFilter.xaml
	/// </summary>
	public partial class GroupedActivityFilter : UserControl
	{
		public GroupedActivityFilterViewModel ViewModel
		{
			get { return (GroupedActivityFilterViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(GroupedActivityFilterViewModel), typeof(GroupedActivityFilter));

		public GroupedActivityFilter()
		{
			InitializeComponent();
		}
	}
}
