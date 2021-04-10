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
    public class LoginViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; set; }
		public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

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
				OnPropertyChanged(nameof(CanLogin));
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
                OnPropertyChanged(nameof(CanLogin));
            }
        }
        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand LoginCommand { get; set; }
        public LoginViewModel(Navigator navigator, IAuthenticator authentificator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);
            LoginCommand = new LoginCommand(this, authentificator, navigator);
        }
    }
}
