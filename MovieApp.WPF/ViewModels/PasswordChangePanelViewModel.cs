using System.Windows.Input;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.ViewModels
{
    public class PasswordChangePanelViewModel : ViewModelBase
    {
        public ICommand ChangePasswordCommand { get; }

        public MessageViewModel ErrorMessageViewModel { get; }
        public MessageViewModel InfoMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public string InfoMessage
        {
            set => InfoMessageViewModel.Message = value;
        }

        private string _oldPassword;

        public string OldPassword
        {
            get
            {
                return _oldPassword;
            }
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
                OnPropertyChanged(nameof(CanChangePassword));
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
                OnPropertyChanged(nameof(CanChangePassword));
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
                OnPropertyChanged(nameof(CanChangePassword));
            }
        }

        public bool CanChangePassword => !string.IsNullOrEmpty(OldPassword) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword);

        public PasswordChangePanelViewModel(IAuthenticator authentificator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            InfoMessageViewModel = new MessageViewModel();

            ChangePasswordCommand = new ChangePasswordCommand(this, authentificator);
        }
    }
}