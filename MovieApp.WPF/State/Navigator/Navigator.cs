using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.State.Navigator
{
    public class Navigator : INavigator
    {
        public event Action StateChanged;
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel { 
            get => _currentViewModel; 
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }       
    }
}
