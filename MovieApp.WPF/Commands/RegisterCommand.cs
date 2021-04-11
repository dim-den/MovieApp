using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly RegisterViewModel _registerViewModel;
        private readonly INavigator _navigator;
        public RegisterCommand(RegisterViewModel loginViewModel, IAuthenticator authenticator, INavigator navigator)
        {
            _registerViewModel = loginViewModel;
            _authenticator = authenticator;
            _navigator = navigator;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return _registerViewModel.CanRegister && base.CanExecute(parameter);    
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticator.Register(
                       _registerViewModel.Username,
                       _registerViewModel.Email,
                       _registerViewModel.Password,
                       _registerViewModel.ConfirmPassword,
                       _registerViewModel.Name,
                       _registerViewModel.Surname);

                _navigator.CurrentViewModel = new HomeViewModel(_navigator);
            }
            catch(PasswordsMismatchException)
            {
                _registerViewModel.ErrorMessage = "Password does not match confirm password.";
            }
            catch(EmailAlreadyExistsException)
            {
                _registerViewModel.ErrorMessage = "An account for this email already exists.";
            }
            catch(UsernameAlreadyExists)
            {
                _registerViewModel.ErrorMessage = "An account for this username already exists.";
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed.";
            }
        }

        private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
