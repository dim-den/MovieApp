using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MovieApp.Domain.Exceptions;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class ChangePasswordCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly PasswordChangePanelViewModel _passwordChangePanelViewModel;

        public override bool CanExecute(object parameter)
        {
            return _passwordChangePanelViewModel.CanChangePassword && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _passwordChangePanelViewModel.ErrorMessage = string.Empty;
            _passwordChangePanelViewModel.InfoMessage = string.Empty;

            try
            {
                await _authenticator.ChangePassword(_passwordChangePanelViewModel.OldPassword, _passwordChangePanelViewModel.NewPassword, _passwordChangePanelViewModel.ConfirmPassword);

                _passwordChangePanelViewModel.InfoMessage = "Your password has been successfully changed.";
            }
            catch (PasswordsMismatchException)
            {
                _passwordChangePanelViewModel.ErrorMessage = "New password does not match confirm password.";
            }
            catch (InvalidPasswordException)
            {
                _passwordChangePanelViewModel.ErrorMessage = "Incorrect old password.";
            }
            catch (Exception)
            {
                _passwordChangePanelViewModel.ErrorMessage = "Change password failed.";
            }
        }

        public ChangePasswordCommand(PasswordChangePanelViewModel passwordChangePanelViewModel, IAuthenticator authenticator)
        {
            _passwordChangePanelViewModel = passwordChangePanelViewModel;
            _authenticator = authenticator;

            _passwordChangePanelViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PasswordChangePanelViewModel.CanChangePassword))
            {
                OnCanExecuteChanged();
            }
        }
    }
}