using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.WPF.Commands;
using MovieApp.WPF.ViewModels;
using MovieApp.WPF.ViewModels.Factories;

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
