using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for GroupFilter.xaml
	/// </summary>
	public partial class GroupFilter : UserControl
	{
		public GroupFilterViewModel ViewModel
		{
			get { return (GroupFilterViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(GroupFilterViewModel), typeof(GroupFilter), new PropertyMetadata(null));

		public GroupFilter()
		{
			InitializeComponent();
		}
	}
}
