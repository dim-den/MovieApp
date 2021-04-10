using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private Navigator _navigator;
        public HomeViewModel(Navigator navigator)
        {
            _navigator = navigator;
        }
    }
}
