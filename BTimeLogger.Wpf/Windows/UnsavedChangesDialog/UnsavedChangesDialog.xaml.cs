using BTimeLogger.Wpf.Util;
using HackerNews.WPF.Core.View;
using System.Windows;
using System.Windows.Data;

namespace BTimeLogger.Wpf.Windows.UnsavedChangesDialog
{
	/// <summary>
	/// Interaction logic for UnsavedChangesDialog.xaml
	/// </summary>
	public partial class UnsavedChangesDialog : Window, IHaveViewModel<UnsavedChangesDialogViewModel>
	{
		public UnsavedChangesDialogViewModel ViewModel { get; set; }

		public UnsavedChangesDialog()
		{
			InitializeComponent();
		}

		public void SetViewModel(UnsavedChangesDialogViewModel viewModel)
		{
			ViewModel = viewModel;

			Binding binding = new("DialogResult");
			binding.Source = ViewModel;
			//binding.Path = new PropertyPath();

			BindingOperations.SetBinding(window, DialogCloser.DialogResultProperty, binding);
			//window.SetBinding(DialogCloser.DialogResultProperty, binding);
			//DialogCloser.SetDialogResult()
		}
	}
}
