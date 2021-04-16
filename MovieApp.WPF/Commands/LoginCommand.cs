using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;
        private readonly INavigator _navigator;

        public override bool CanExecute(object parameter)
        {
            return _loginViewModel.CanLogin && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
               await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);

                var filmStore = new Store<Film>();
                var actorStore = new Store<Actor>();

                await filmStore.Load();
                await actorStore.Load();

               _navigator.CurrentViewModel = new HomeViewModel(_navigator, filmStore, actorStore); 
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed.";
            }
        }

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, INavigator navigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _navigator = navigator;

            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        }

        private void LoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
