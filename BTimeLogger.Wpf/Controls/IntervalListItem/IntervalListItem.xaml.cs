using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for IntervalListItem.xaml
	/// </summary>
	public partial class IntervalListItem : UserControl
	{
		public IntervalListItemViewModel ViewModel
		{
			get { return (IntervalListItemViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(IntervalListItemViewModel), typeof(IntervalListItem));

		public IntervalListItem()
		{
			InitializeComponent();
		}
	}
}
