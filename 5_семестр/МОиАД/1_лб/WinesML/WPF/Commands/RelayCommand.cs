using System;
using System.Windows.Input;

namespace WPF.Commands
{
	class RelayCommand : ICommand
	{
		private Action execute;
		private Func<bool> canExecute;

		public RelayCommand(Action execute, Func<bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value; 
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute?.Invoke() ?? true;
		}

		public void Execute(object parameter)
		{
			execute.Invoke();
		}
	}
}
