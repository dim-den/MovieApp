using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly RegisterViewModel _registerViewModel;
        public RegisterCommand(RegisterViewModel loginViewModel, AuthenticationService authenticationService)
        {
            _registerViewModel = loginViewModel;
            _authenticationService = authenticationService;

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
                RegistrationResult registrationResult = await _authenticationService.Register(
                       _registerViewModel.Username,
                       _registerViewModel.Email,
                       _registerViewModel.Password,
                       _registerViewModel.ConfirmPassword,
                       _registerViewModel.Name,
                       _registerViewModel.Surname);

                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        // _registerRenavigator.Renavigate(); TODO
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Password does not match confirm password.";
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        _registerViewModel.ErrorMessage = "An account for this email already exists.";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _registerViewModel.ErrorMessage = "An account for this username already exists.";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Registration failed.";
                        break;
                }
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
