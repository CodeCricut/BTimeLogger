using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCore.Commands
{
	/// <summary>
	/// Simple implemenation of <see cref="ICommand"/> which executes an asynchronous delegate action
	/// when the command is executed.
	/// </summary>
	public class AsyncDelegateCommand : ICommand
	{
		private readonly Func<object, Task> _execute;
		private readonly Func<object, bool> _canExecute;

		public AsyncDelegateCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
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

		public async void Execute(object parameter = null)
		{
			await _execute?.Invoke(parameter);
		}
	}
}
