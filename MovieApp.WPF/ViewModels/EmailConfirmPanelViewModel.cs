using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.ViewModels
{
    public class EmailConfirmPanelViewModel : ViewModelBase
    {
        public ICommand SendEmailConfirmCodeCommand { get; }

        public MessageViewModel ErrorMessageViewModel { get; }

        private const int verificationCodeLength = 4;
        private readonly IAuthenticator _authentificator;
        private readonly IUnitOfWork _unitOfWork;

        public string ExpectedVerificationCode { get; set; } = string.Empty;

        private string _verificationCode = string.Empty;
        public string VerificationCode
        {
            get => _verificationCode;
            set
            {
                _verificationCode = value;
                OnPropertyChanged(nameof(VerificationCode));

                if (_verificationCode == ExpectedVerificationCode)
                {
                    CurrentUser.ConfirmedEmail = true;
                    _authentificator.CurrentUser = CurrentUser;

                    OnPropertyChanged(nameof(CurrentUser));
                    OnPropertyChanged(nameof(UserConfirmedEmail));

                    _unitOfWork.UserRepository.Update(CurrentUser.ID, CurrentUser).Wait();
                    _unitOfWork.SaveAsync().Start();
                }
            }
        }

        public User CurrentUser => _authentificator.CurrentUser;
        public bool UserConfirmedEmail => _authentificator.CurrentUser.ConfirmedEmail;

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public EmailConfirmPanelViewModel(IAuthenticator authenticator, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _authentificator = authenticator;
            _unitOfWork = unitOfWork;

            SendEmailConfirmCodeCommand = new SendEmailConfirmCodeCommand(this, emailService, verificationCodeLength);

            ErrorMessageViewModel = new MessageViewModel();

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
        }
    }
}
