using BTimeLogger.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for PartialIntervalList.xaml
	/// </summary>
	public partial class PartialIntervalList : UserControl
	{
		public PartialIntervalListViewModel ViewModel
		{
			get { return (PartialIntervalListViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(PartialIntervalListViewModel), typeof(PartialIntervalList), new PropertyMetadata(null));

		public PartialIntervalList()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel.ItemsReset += ViewModel_ItemsReset;
		}

		private void ListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (ScrollBarHitBottom(e))
				TryLoadMoreItems();
		}

		private void TryLoadMoreItems()
		{
			if (ViewModel.LoadMoreItemsCommand.CanExecute())
				ViewModel.LoadMoreItemsCommand.Execute();
		}

		private void ViewModel_ItemsReset(object sender, System.EventArgs e)
		{
			ResetScrollbarPosition();
		}

		private void ResetScrollbarPosition()
		{
			// Get the border of the listview (first child of a listview).
			if (VisualTreeHelper.GetChild(listView, 0) is Decorator border &&
				border.Child is ScrollViewer scrollViewer)
			{
				scrollViewer.ScrollToVerticalOffset(0);
			}
		}

		private static bool ScrollBarHitBottom(ScrollChangedEventArgs e) => e.VerticalChange > 0 && e.VerticalOffset + e.ViewportHeight == e.ExtentHeight;
	}
}
