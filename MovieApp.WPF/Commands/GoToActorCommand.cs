using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class GoToActorCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;
        public override async Task ExecuteAsync(object parameter)
        {
            Actor actor = (Actor)parameter;

            var actorFilmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
            await actorFilmCastStore.LoadWithInclude(c => c.ActorID == actor.ID, c => c.Film);

            _navigator.CurrentViewModel = new ActorViewModel(_navigator, _authenticator, actor, actorFilmCastStore);
        }

        public GoToActorCommand(INavigator navigator, IAuthenticator authentificator, IUnitOfWork unitOfWork)
        {
            _navigator = navigator;
            _authenticator = authentificator;
            _unitOfWork = unitOfWork;
        }
    }
}
