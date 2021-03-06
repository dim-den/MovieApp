using System.Windows.Input;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }

        private string _email;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanRegister));
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
                OnPropertyChanged(nameof(CanRegister));
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
                OnPropertyChanged(nameof(CanRegister));
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
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _surname;

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
                                   !string.IsNullOrEmpty(Username) &&
                                   !string.IsNullOrEmpty(Password) &&
                                   !string.IsNullOrEmpty(ConfirmPassword) &&
                                   !string.IsNullOrEmpty(Name) &&
                                   !string.IsNullOrEmpty(Surname);

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(IAuthenticator authentificator, ICommand changeViewCommand, IRenavigator registerRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ChangeViewCommand = changeViewCommand;
            RegisterCommand = new RegisterCommand(this, authentificator, registerRenavigator);
        }
    }
}