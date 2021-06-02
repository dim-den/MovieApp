using System;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.State.Navigator
{
    public class Navigator : INavigator
    {
        public event Action StateChanged;

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (value != null)
                {
                    _currentViewModel = value;
                    StateChanged?.Invoke();
                }
            }
        }
    }
}