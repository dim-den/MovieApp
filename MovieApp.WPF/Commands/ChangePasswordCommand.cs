using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Exceptions;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class ChangePasswordCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly SettingsViewModel _settingsViewModel;

        public override bool CanExecute(object parameter)
        {
            return _settingsViewModel.CanLogin && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _settingsViewModel.ErrorMessage = string.Empty;
            _settingsViewModel.InfoMessage = string.Empty;

            try
            {
                await _authenticator.ChangePassword(_settingsViewModel.OldPassword, _settingsViewModel.NewPassword, _settingsViewModel.ConfirmPassword);

                _settingsViewModel.InfoMessage = "Your password has been successfully changed.";
            }
            catch (PasswordsMismatchException)
            {
                _settingsViewModel.ErrorMessage = "New password does not match confirm password.";
            }
            catch (InvalidPasswordException)
            {
                _settingsViewModel.ErrorMessage = "Incorrect old password.";
            }
            catch (Exception)
            {
                _settingsViewModel.ErrorMessage = "Change password failed.";
            }
        }

        public ChangePasswordCommand(SettingsViewModel settingsViewModel, IAuthenticator authenticator)
        {
            _settingsViewModel = settingsViewModel;
            _authenticator = authenticator;

            _settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsViewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
