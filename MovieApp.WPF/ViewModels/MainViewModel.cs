using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNet.Identity;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

    namespace MovieApp.WPF.ViewModels
    {
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public User CurrentUser => _authenticator.CurrentUser;

        public AppHeaderViewModel AppHeaderViewModel { get; }
        public MainViewModel()
        {
            _navigator = new Navigator();
            _unitOfWork = new UnitOfWork();
            _authenticator = new Authenticator(new AuthenticationService(_unitOfWork, new PasswordHasher()), new Account());

            _navigator.CurrentViewModel = new LoginViewModel(_navigator, _authenticator);
            AppHeaderViewModel = new AppHeaderViewModel(_navigator, _authenticator, _unitOfWork);

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
