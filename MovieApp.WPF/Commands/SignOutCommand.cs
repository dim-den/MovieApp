using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class SignOutCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        public SignOutCommand(INavigator navigator, IAuthenticator authenticator = null)
        {
            _navigator = navigator;
            _authenticator = authenticator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _authenticator.IsLoggedIn;
        }

        public void Execute(object parameter)
        {
            _authenticator.Logout();
            _navigator.CurrentViewModel = new LoginViewModel(_navigator, _authenticator);

        }
    }
}
