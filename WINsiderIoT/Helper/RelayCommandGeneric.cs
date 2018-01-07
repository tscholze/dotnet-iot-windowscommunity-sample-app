using System;
using System.Windows.Input;

namespace WINsiderIoT.Helper
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _handler;
        private readonly Func<T, bool> _canExecute;

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value == _isEnabled)
                    return;

                _isEnabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> handler, Func<T, bool> canExecute = null)
        {
            _handler = handler;
            _canExecute = canExecute;
            if (canExecute == null)
                _isEnabled = true;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                IsEnabled = _canExecute((T)parameter);

            return IsEnabled;
        }

        public void Execute(object parameter)
        {
            _handler((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
