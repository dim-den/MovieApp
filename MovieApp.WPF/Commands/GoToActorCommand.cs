using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class GoToActorCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IUnitOfWork _unitOfWork;
        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Actor actor = (Actor)parameter;

            var actorFilmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
            await actorFilmCastStore.LoadWithInclude(c => c.ActorID == actor.ID, c => c.Film);

            _navigator.CurrentViewModel = new ActorViewModel(actor, actorFilmCastStore);
        }

        public GoToActorCommand(INavigator navigator, IUnitOfWork unitOfWork)
        {
            _navigator = navigator;
            _unitOfWork = unitOfWork;
        }
    }
}
