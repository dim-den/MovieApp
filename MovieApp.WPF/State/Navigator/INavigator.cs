using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.State.Navigator
{
    public enum ViewType
    {
        Login,
        Register,
        Home
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
