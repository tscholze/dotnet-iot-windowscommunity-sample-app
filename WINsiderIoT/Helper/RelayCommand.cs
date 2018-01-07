using System;
using System.Windows.Input;

namespace WINsiderIoT.Helper
{
    public class RelayCommand : ICommand
    {
        private readonly Action _handler;
        private readonly Func<bool> _canExecute;
        
        private bool _isEnabled;
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

        public RelayCommand(Action handler, Func<bool> canExecute = null)
        {
            _handler = handler;
            _canExecute = canExecute;

            if (canExecute == null)
                _isEnabled = true;
        }
        
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                IsEnabled = _canExecute();

            return IsEnabled;
        }
        
        public void Execute(object parameter)
        {
            _handler();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }    
}
