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
    public class HomeViewModelBuilder : IViewModelBuilder<HomeViewModel>
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;

        private IStore<Film> _randomFilms;
        private IStore<Actor> _randomActors;
        private IStore<Film> _upcomingFilms;

        private HomeViewModelBuilder(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _unitOfWork = unitOfWork;
        }

        public static HomeViewModelBuilder Init(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork)
        {
            return new HomeViewModelBuilder(navigator, authenticator, unitOfWork);
        }

        public async Task<HomeViewModelBuilder> LoadRandomFilms(int count)
        {
            _randomFilms = new Store<Film>(await _unitOfWork.FilmRepository.GetRandomReleasedFilms(count));

            return this;
        }

        public async Task<HomeViewModelBuilder> LoadRandomActors(int count)
        {
            _randomActors = new Store<Actor>(await _unitOfWork.ActorRepository.GetRandomEntities(count));

            return this;
        }

        public async Task<HomeViewModelBuilder> LoadUpcomingFilms(int count)
        {
            _upcomingFilms = new Store<Film>((await _unitOfWork.FilmRepository.GetUpcomingFilms()).Take(count).ToList());

            return this;
        }

        public HomeViewModel Build()
        {
            return null;
        }
    }
}
