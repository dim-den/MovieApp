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

namespace MovieApp.WPF.ViewModels.Builders
{
    public class ActorViewModelBuilder : IViewModelBuilder<ActorViewModel>
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Actor _actor;

        private IStore<FilmCast> _filmCastStore;

        public async Task<ActorViewModelBuilder> LoadFilmCast()
        {
            await _filmCastStore.LoadWithInclude(c => c.ActorID == _actor.ID, c => c.Film);

            return this;
        }

        private ActorViewModelBuilder(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork, Actor actor)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _unitOfWork = unitOfWork;
            _actor = actor;

            _filmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
        }

        public static ActorViewModelBuilder Init(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork, Actor actor)
        {
            return new ActorViewModelBuilder(navigator, authenticator, unitOfWork, actor);
        }

        public ActorViewModel Build()
        {
            return new ActorViewModel(_navigator,
                                     _authenticator,
                                     _actor,
                                     _filmCastStore);
        }
    }
}
