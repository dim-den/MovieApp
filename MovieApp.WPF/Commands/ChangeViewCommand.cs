using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;
using MovieApp.WPF.ViewModels.Factories;

namespace MovieApp.WPF.Commands
{
    public class ChangeViewCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        public ChangeViewCommand(INavigator navigator, IAuthenticator authenticator = null)
        {
            _navigator = navigator;
            _viewModelFactory = new ViewModelFactory(navigator, authenticator);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _navigator.CurrentViewModel = await _viewModelFactory.CreateViewModel(parameter);
        }
    }
}
