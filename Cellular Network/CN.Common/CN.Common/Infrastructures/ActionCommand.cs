using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CN.Common.Infrastructures
{
    public class ActionCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public ActionCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }
    }

    public class ActionCommand<T> : ICommand
    {
        private Action<T> _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public ActionCommand(Action<T> action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _action?.Invoke((T)parameter);
        }
    }
}
