using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace BTimeLogger.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for FilePicker.xaml
	/// </summary>
	public partial class FilePicker : UserControl
	{
		public string Text { get { return GetValue(TextProperty) as string; } set { SetValue(TextProperty, value); } }
		public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FilePicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string Description { get { return GetValue(DescriptionProperty) as string; } set { SetValue(DescriptionProperty, value); } }
		public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(FilePicker), new PropertyMetadata(null));

		public FilePicker() { InitializeComponent(); }

		private void BrowseFolder(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new();
			bool? result = openFileDialog.ShowDialog();
			if (result == true)
			{
				Text = openFileDialog.FileName;
			}
		}
	}
}
