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
    public class MainViewModel : ViewModelBase
    {
        private readonly Navigator _navigator;
        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;              

        public MainViewModel()
        {
            _navigator = new Navigator();
            _authenticator = new Authenticator(new AuthenticationService(new UserDataService()), new Account());

            _navigator.CurrentViewModel = new LoginViewModel(_navigator, _authenticator);

            _navigator.StateChanged += OnCurrentViewModelChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
}
    