using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class FilmViewModelBuilder : IViewModelBuilder<FilmViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;

        private RateFilmPanelViewModelBuilder _rateFilmPanelViewModelBuilder;
        private UserReviewsPanelViewModelBuilder _userReviewsPanelViewModelBuilder;
        private FilmCastListViewModelBuilder _filmCastListViewModelBuilder;

        private FilmViewModel _filmViewModel;

        private FilmViewModelBuilder(RateFilmPanelViewModelBuilder rateFilmPanelViewModelBuilder,
                                     UserReviewsPanelViewModelBuilder userReviewsPanelViewModelBuilder,
                                     FilmCastListViewModelBuilder filmCastListViewModelBuilder,
                                     IUnitOfWork unitOfWork, IAuthenticator authenticator)
        {
            _rateFilmPanelViewModelBuilder = rateFilmPanelViewModelBuilder;
            _userReviewsPanelViewModelBuilder = userReviewsPanelViewModelBuilder;
            _filmCastListViewModelBuilder = filmCastListViewModelBuilder;

            _unitOfWork = unitOfWork;
            _authenticator = authenticator;

            _filmViewModel = new FilmViewModel(_unitOfWork);
        }

        public FilmViewModelBuilder SetFilm(Film film)
        {
            _filmViewModel.Film = film;
            LoadCurrentUserFilmReview();

            _filmViewModel.RateFilmPanelViewModel = _rateFilmPanelViewModelBuilder.SetFilm(film).Build();
            _filmViewModel.UserReviewsPanelViewModel = _userReviewsPanelViewModelBuilder.SetFilm(film).Build();
            _filmViewModel.FilmCastListViewModel = _filmCastListViewModelBuilder.SetFilm(film).Build();

            return this;
        }
        public static FilmViewModelBuilder Init(RateFilmPanelViewModelBuilder rateFilmPanelViewModelBuilder, 
                                                UserReviewsPanelViewModelBuilder userReviewsPanelViewModelBuilder,
                                                FilmCastListViewModelBuilder filmCastListViewModelBuilder,
                                                IUnitOfWork unitOfWork, IAuthenticator authenticator)
        {
            return new FilmViewModelBuilder(rateFilmPanelViewModelBuilder, userReviewsPanelViewModelBuilder, 
                                            filmCastListViewModelBuilder, unitOfWork, authenticator);
        }

        public FilmViewModel Build()
        {
            return _filmViewModel;
        }

        private void LoadCurrentUserFilmReview()
        {
            _unitOfWork.FilmReviewRepository.GetUserFilmReview(_authenticator.CurrentUser.ID, _filmViewModel.Film.ID).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    var currentUserFilmReview = task.Result;
                    _filmViewModel.CurrentUserFilmReview = currentUserFilmReview;

                    _filmViewModel.RateFilmPanelViewModel.CurrentUserFilmReview = currentUserFilmReview;
                    _filmViewModel.UserReviewsPanelViewModel.CurrentUserFilmReview = currentUserFilmReview;

                }
            });
        }
    }
}
