using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class AppHeaderViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authentificator;

        public ICommand ChangeViewCommand { get; }

        public ICommand SignOutCommand { get; }

        public User CurrentUser => _authentificator.CurrentUser;

        public bool IsAdmin => CurrentUser != null && CurrentUser.ClientType >= ClientType.Admin;
        
        public byte[] ImageData => CurrentUser?.ImageData;

        public AppHeaderViewModel(INavigator navigator, IAuthenticator authentificator)
        {
            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);
            SignOutCommand = new SignOutCommand(navigator, authentificator);

            _authentificator = authentificator;

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(ImageData));
            OnPropertyChanged(nameof(IsAdmin));
        }
    }
}