using System;
using System.Windows.Input;

namespace OpenCVTester.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return (this._canExecute == null) ? true : this._canExecute();
        }    
        public void Execute(object parameter)
        {
            this._execute();
        }
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public DelegateCommand(Action execute) : this(execute, null)
        {

        }
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
    }
}
