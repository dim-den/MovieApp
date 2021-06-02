using System.Windows.Input;
using Microsoft.AspNet.Identity;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.Commands;

namespace MovieApp.WPF.ViewModels
{
    public enum PasswordRecoveryStage
    {
        PrintUsername,
        PrintVerificationCode,
        CreateNewPassword,
        SuccessfullyChanged
    };

    public class PasswordRecoveryViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }
        public ICommand NextStepPasswordRecoveryCommand { get; }
        public ICommand UpdateUserPasswordCommand { get; }

        public string ExpectedVerificationCode { get; set; }

        private PasswordRecoveryStage _passwordRecoveryStage;

        public PasswordRecoveryStage PasswordRecoveryStage
        {
            get => _passwordRecoveryStage;
            set
            {
                _passwordRecoveryStage = value;
                OnPropertyChanged(nameof(PasswordRecoveryStage));
            }
        }

        private string _username;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _verificationCode;

        public string VerificationCode
        {
            get
            {
                return _verificationCode;
            }
            set
            {
                _verificationCode = value;
                OnPropertyChanged(nameof(VerificationCode));

                if (VerificationCode == ExpectedVerificationCode)
                {
                    PasswordRecoveryStage = PasswordRecoveryStage.CreateNewPassword;
                }
            }
        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public MessageViewModel ErrorUsernameMessageViewModel { get; }

        public string ErrorUsernameMessage
        {
            set => ErrorUsernameMessageViewModel.Message = value;
        }

        public MessageViewModel ErrorPasswordMessageViewModel { get; }

        public string ErrorPasswordMessage
        {
            set => ErrorPasswordMessageViewModel.Message = value;
        }

        public PasswordRecoveryViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand, IEmailService emailService, IPasswordHasher passwordHasher)
        {
            ErrorUsernameMessageViewModel = new MessageViewModel();
            ErrorPasswordMessageViewModel = new MessageViewModel();
            ChangeViewCommand = changeViewCommand;

            NextStepPasswordRecoveryCommand = new NextStepPasswordRecoveryCommand(this, unitOfWork, emailService);
            UpdateUserPasswordCommand = new UpdateUserPasswordCommand(this, unitOfWork, passwordHasher);
        }
    }
}