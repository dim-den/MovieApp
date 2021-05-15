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
        PasswordRecovery,
        Home,
        Profile,
        Film,
        Actor,
        AdminPanel,
        Settings
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
