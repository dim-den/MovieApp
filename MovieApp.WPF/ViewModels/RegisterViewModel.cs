using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase 
    {
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
        public ICommand ChangeViewCommand { get; set; }

        public RegisterViewModel(INavigator navigator, IAuthenticator authentificator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);
            RegisterCommand = new RegisterCommand(this, authentificator, navigator);
        }
    }
}
