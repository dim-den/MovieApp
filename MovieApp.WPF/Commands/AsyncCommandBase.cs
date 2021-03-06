using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MovieApp.WPF.State.NetworkState;

namespace MovieApp.WPF.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        private bool _isExecuting;

        protected AsyncCommandBase()
        {
            NetworkState.StateChanged += OnCanExecuteChanged;
        }

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return !IsExecuting && NetworkState.IsInternetAvailable;
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;

            await ExecuteAsync(parameter);

            IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object parameter);

        protected void OnCanExecuteChanged()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => CanExecuteChanged?.Invoke(this, new EventArgs())));
        }
    }
}