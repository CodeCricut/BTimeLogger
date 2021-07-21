using BTimeLogger.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for IntervalList.xaml
	/// </summary>
	public partial class IntervalList : UserControl
	{
		public IntervalListViewModel ViewModel
		{
			get { return (IntervalListViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(IntervalListViewModel), typeof(IntervalList));

		public IntervalList()
		{
			InitializeComponent();
		}
	}
}
