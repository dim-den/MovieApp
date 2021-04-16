using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class ChangeViewCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        public ChangeViewCommand(INavigator navigator, IAuthenticator authenticator = null)
        {
            _navigator = navigator;
            _authenticator = authenticator;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ViewType viewType = (ViewType)parameter;
            switch (viewType)
            {
                case ViewType.Login:
                    _navigator.CurrentViewModel = new LoginViewModel(_navigator, _authenticator);
                    break;
                case ViewType.Register:
                    _navigator.CurrentViewModel = new RegisterViewModel(_navigator, _authenticator);
                    break;
                case ViewType.Home:
                    var filmStore = new Store<Film>();
                    var actorStore = new Store<Actor>();

                    await filmStore.Load();
                    await actorStore.Load();

                    _navigator.CurrentViewModel = new HomeViewModel(_navigator, filmStore, actorStore);
                    break;
                case ViewType.Profile:
                    // TODO
                    break;
            }
        }
    }
}
