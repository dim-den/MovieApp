using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class NextStepPasswordRecoveryCommand : AsyncCommandBase
    {
        private const int _verificationCodeLength = 8;

        private readonly PasswordRecoveryViewModel _passwordRecoveryViewModel;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_passwordRecoveryViewModel.Username) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _passwordRecoveryViewModel.ErrorUsernameMessage = string.Empty;

            User user = await _unitOfWork.UserRepository.GetByUsername(_passwordRecoveryViewModel.Username);

            if (user == null)
            {
                _passwordRecoveryViewModel.ErrorUsernameMessage = "Wrong username!";
            }
            else if (user.ConfirmedEmail == false)
            {
                _passwordRecoveryViewModel.ErrorUsernameMessage = "Can't recover password, because user hasn't confirmed email.";
            }
            else
            {
                try
                {
                    string body = "<p>Hey {0}, type this verification code in application to recover your password:</p>" +
                                      "<b>{1}</b><br>" +
                                      "<p>If that was not you recovering password, then you have to do something about this.</p>";
                    string subject = "Password recovery";

                    _passwordRecoveryViewModel.ExpectedVerificationCode = await _emailService.SendMessage(user, body, subject, _verificationCodeLength);

                    _passwordRecoveryViewModel.PasswordRecoveryStage = PasswordRecoveryStage.PrintVerificationCode;
                }
                catch(Exception)
                {
                    _passwordRecoveryViewModel.ErrorUsernameMessage = "An error occured while sending message, check for Internet connection";
                }
            }
        }

        public NextStepPasswordRecoveryCommand(PasswordRecoveryViewModel passwordRecoveryViewModel, IUnitOfWork unitOfWork)
        {
            _passwordRecoveryViewModel = passwordRecoveryViewModel;   
            _unitOfWork = unitOfWork;
            _emailService = new EmailService();

            _passwordRecoveryViewModel.PropertyChanged += PasswordRecoveryViewModel_PropertyChanged;
        }

        private void PasswordRecoveryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PasswordRecoveryViewModel.Username))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
