using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.View
{
	/// <summary>
	/// Interaction logic for MultiSelectListBox.xaml
	/// https://stackoverflow.com/a/22908694
	/// </summary>
	public partial class MultiSelectListBox : ListBox
	{
		public MultiSelectListBox()
		{
			SelectionChanged += ListBoxCustom_SelectionChanged;
		}

		void ListBoxCustom_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedItemsList.Clear();
			foreach (var item in SelectedItems)
			{
				SelectedItemsList.Add(item);
			}
		}

		public IList SelectedItemsList
		{
			get { return (IList)GetValue(SelectedItemsListProperty); }
			set { SetValue(SelectedItemsListProperty, value); }
		}

		public static readonly DependencyProperty SelectedItemsListProperty =
		   DependencyProperty.Register(nameof(SelectedItemsList), typeof(IList), typeof(MultiSelectListBox), new PropertyMetadata(null));
	}
}
