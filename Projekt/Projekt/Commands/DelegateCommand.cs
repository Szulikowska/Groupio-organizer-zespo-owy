using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Projekt.Commands
{
    public class DelegateCommand : ICommand
    {
        #region Properties
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;
        #endregion

        public DelegateCommand(Action ex) : this(ex, null) { }
        public DelegateCommand(Action ex, Func<bool> canEx)
        {
            _execute = ex;
            _canExecute = canEx;
        }

        public bool CanExecute(object param)
        {
            if (_canExecute != null)
                return _canExecute();

            return true;
        }

        public void Execute(object param)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
