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
    public class GoToFilmCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;
        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Film film = (Film)parameter;

            var filmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
            var filmReviewStore = new Store<FilmReview>(_unitOfWork.FilmReviewRepository);

            await filmCastStore.LoadWithInclude(f => f.FilmID == film.ID, f => f.Actor);
            await filmReviewStore.LoadWithInclude(r => r.FilmID == film.ID && !string.IsNullOrEmpty(r.ReviewText), r => r.User);
            var userFilmReview = await _unitOfWork.FilmReviewRepository.GetUserFilmReview(_authenticator.CurrentUser.ID, film.ID);

            _navigator.CurrentViewModel = new FilmViewModel(_navigator,
                                                            _authenticator,
                                                            filmCastStore,
                                                            filmReviewStore,
                                                            film,
                                                            userFilmReview);
        }

        public GoToFilmCommand(INavigator navigator, IAuthenticator authentificator, IUnitOfWork unitOfWork)
        {
            _navigator = navigator;
            _authenticator = authentificator;
            _unitOfWork = unitOfWork;
        }
    }
}
