using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(object viewType);
    }
}
