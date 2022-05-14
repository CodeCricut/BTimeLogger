using System;

namespace BTimeLogger.Wpf.Controls;

public class WindowButtonsViewModel : BaseViewModel
{
	public event EventHandler Minimized;
	public event EventHandler Maximized;
	public event EventHandler Restored;
	public event EventHandler Closed;

	private bool _isMaximized;
	public bool IsMaximized
	{
		get => _isMaximized;
		set
		{
			Set(ref _isMaximized, value);
			RaisePropertyChanged(nameof(IsNotMaximized));
			MinimizeCommand.RaiseCanExecuteChanged();
			RestoreCommand.RaiseCanExecuteChanged();
		}
	}

	public bool IsNotMaximized => !IsMaximized;

	public DelegateCommand MinimizeCommand { get; set; }
	public DelegateCommand MaximizeCommand { get; set; }
	public DelegateCommand RestoreCommand { get; set; }
	public DelegateCommand CloseCommand { get; set; }

	public WindowButtonsViewModel()
	{
		MinimizeCommand = new DelegateCommand(Minimize);
		MaximizeCommand = new DelegateCommand(Maximize, _ => IsNotMaximized);
		RestoreCommand = new DelegateCommand(Restore, _ => IsMaximized);
		CloseCommand = new DelegateCommand(Close);
	}

	public void Minimize(object param = null)
	{
		Minimized?.Invoke(this, new());
	}

	public void Maximize(object param = null)
	{
		IsMaximized = true;
		Maximized?.Invoke(this, new());
	}

	public void Restore(object param = null)
	{
		IsMaximized = false;
		Restored?.Invoke(this, new());
	}

	public void Close(object param = null)
	{
		Closed?.Invoke(this, new());
	}
}
