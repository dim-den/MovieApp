using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class UpdateUserPasswordCommand : AsyncCommandBase
    {
        private readonly PasswordRecoveryViewModel _passwordRecoveryViewModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthenticationService _authenticationService;

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_passwordRecoveryViewModel.Password) 
                && !string.IsNullOrEmpty(_passwordRecoveryViewModel.ConfirmPassword) 
                && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _passwordRecoveryViewModel.ErrorPasswordMessage = string.Empty;

            if(_passwordRecoveryViewModel.Password != _passwordRecoveryViewModel.ConfirmPassword)
            {
                _passwordRecoveryViewModel.ErrorPasswordMessage = "Password does not match confirm password.";
                return;
            }

            User user = await _unitOfWork.UserRepository.GetByUsername(_passwordRecoveryViewModel.Username);
            string hashedPassword = _passwordHasher.HashPassword(_passwordRecoveryViewModel.Password);
            user.PasswordHash = hashedPassword;

            await _unitOfWork.UserRepository.Update(user.ID, user);

            await _unitOfWork.SaveAsync();

            _passwordRecoveryViewModel.PasswordRecoveryStage = PasswordRecoveryStage.SuccessfullyChanged;
        }

        public UpdateUserPasswordCommand(PasswordRecoveryViewModel passwordRecoveryViewModel, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _passwordRecoveryViewModel = passwordRecoveryViewModel;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;

            _passwordRecoveryViewModel.PropertyChanged += PasswordRecoveryViewModel_PropertyChanged;
        }

        private void PasswordRecoveryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PasswordRecoveryViewModel.Password) || e.PropertyName == nameof(PasswordRecoveryViewModel.ConfirmPassword))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
