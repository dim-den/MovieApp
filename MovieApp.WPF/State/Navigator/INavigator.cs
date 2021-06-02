using System;
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