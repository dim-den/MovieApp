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
    public class FilmViewModelBuilder : IViewModelBuilder<FilmViewModel>
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Film _film;

        private IStore<FilmCast> _filmCastStore;
        private IStore<FilmReview> _filmReviewStore;
        private FilmReview _userFilmReview;

        public async Task<FilmViewModelBuilder> LoadFilmCast()
        {
            await _filmCastStore.LoadWithInclude(f => f.FilmID == _film.ID, f => f.Actor);

            return this;
        }

        public async Task<FilmViewModelBuilder> LoadFilmReviews()
        {
            await _filmReviewStore.LoadWithInclude(r => r.FilmID == _film.ID && !string.IsNullOrEmpty(r.ReviewText), r => r.User);

            return this;
        }

        public async Task<FilmViewModelBuilder> LoadCurrentUserFilmReview()
        {
            _userFilmReview = await _unitOfWork.FilmReviewRepository.GetUserFilmReview(_authenticator.CurrentUser.ID, _film.ID);

            return this;
        }

        private FilmViewModelBuilder(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork, Film film)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _unitOfWork = unitOfWork;
            _film = film;

            _filmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
            _filmReviewStore = new Store<FilmReview>(_unitOfWork.FilmReviewRepository);
        }

        public static FilmViewModelBuilder Init(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork, Film film)
        {
            return new FilmViewModelBuilder(navigator, authenticator, unitOfWork, film);
        }

        public FilmViewModel Build()
        {
            return new FilmViewModel(_navigator,
                                     _authenticator,
                                     _filmCastStore,
                                     _filmReviewStore,
                                     _film,
                                     _userFilmReview);
        }
    }
}
