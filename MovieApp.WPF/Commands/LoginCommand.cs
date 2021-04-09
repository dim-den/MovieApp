using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticationService _authenticationService;
        private readonly LoginViewModel _loginViewModel;

        public override bool CanExecute(object parameter)
        {
            return _loginViewModel.CanLogin && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
               await _authenticationService.Login(_loginViewModel.Username, _loginViewModel.Password);

               // _renavigator.Renavigate(); TODO
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

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticationService authenticationService)
        {
            _loginViewModel = loginViewModel;
            _authenticationService = authenticationService;

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
