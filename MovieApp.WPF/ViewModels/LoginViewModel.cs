using System.Windows.Input;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }
        public ICommand LoginCommand { get; }
        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        private string _username = "dimden";

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
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string _password = "admin";

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
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public LoginViewModel(IAuthenticator authentificator, ICommand changeViewCommand, IRenavigator loginRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            ChangeViewCommand = changeViewCommand;

            LoginCommand = new LoginCommand(this, authentificator, loginRenavigator);
        }
    }
}