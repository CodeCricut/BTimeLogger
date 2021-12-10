using System;
using System.Windows.Input;

namespace WpfCore.Commands
{
	/// <summary>
	/// Simple implemenation of <see cref="ICommand"/> which executes a delegate action
	/// when the command is executed.
	/// </summary>
	public class DelegateCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Func<object, bool> _canExecute;

		public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanExecute(object parameter = null)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public void Execute(object parameter = null)
		{
			_execute?.Invoke(parameter);
		}
	}
}
