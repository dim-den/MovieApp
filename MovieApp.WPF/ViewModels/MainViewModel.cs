using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using MovieApp.WPF.State.NetworkState;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels.Factories;

namespace MovieApp.WPF.ViewModels
    {
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;

        public ICommand ChangeViewCommand { get; }

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public bool IsInternetAvailable => NetworkState.IsInternetAvailable;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public User CurrentUser => _authenticator.CurrentUser;

        public AppHeaderViewModel AppHeaderViewModel { get; }
        public MainViewModel(INavigator navigator, IUnitOfWork unitOfWork, IAuthenticator authenticator,
                             ChangeViewCommand changeViewCommand, AppHeaderViewModel appHeaderViewModel)
        {
            _navigator = navigator;
            _unitOfWork = unitOfWork;
            _authenticator = authenticator;

            ChangeViewCommand = changeViewCommand;

            AppHeaderViewModel = appHeaderViewModel;

            ChangeViewCommand.Execute(ViewType.Login);

            _navigator.StateChanged += OnCurrentViewModelChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;
            NetworkState.StateChanged += Network_StateChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private void Network_StateChanged()
        {
            OnPropertyChanged(nameof(IsInternetAvailable));
        }
    }
}
