using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eventmaker.Common
{
    class RelayArgCommand<T> : ICommand
    {

        // Looks like since we are using a List<Event>, in case we'd like to work with it this is kind of RelayCommand to it?

        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;


        public event EventHandler CanExecuteChanged;

        public RelayArgCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        public RelayArgCommand(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        internal Handler.EventHandler EventHandler
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool CanExecute(object parameter)
        {
            if (!typeof(T).Equals(parameter.GetType())) return false;
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
