using System;
using System.Windows.Input;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.ViewModels.Factories;

namespace MovieApp.WPF.Commands
{
    public class ChangeViewCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public ChangeViewCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(parameter);
        }
    }
}