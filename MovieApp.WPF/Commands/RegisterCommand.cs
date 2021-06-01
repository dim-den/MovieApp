using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;
using MovieApp.WPF.ViewModels.Builders;

namespace MovieApp.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly RegisterViewModel _registerViewModel;
        private readonly IRenavigator _renavigator;

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

                _renavigator.Renavigate();
            }
            catch (PasswordsMismatchException)
            {
                _registerViewModel.ErrorMessage = "Password does not match confirm password.";
            }
            catch (EmailAlreadyExistsException)
            {
                _registerViewModel.ErrorMessage = "An account for this email already exists.";
            }
            catch (UsernameAlreadyExistsException)
            {
                _registerViewModel.ErrorMessage = "An account for this username already exists.";
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed.";
            }
        }
        public RegisterCommand(RegisterViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _registerViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
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
