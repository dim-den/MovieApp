using System;
using System.ComponentModel;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels
{
    public class FilmViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private UserReviewsPanelViewModel _userReviewsPanelViewModel;

        public UserReviewsPanelViewModel UserReviewsPanelViewModel
        {
            get => _userReviewsPanelViewModel;
            set
            {
                _userReviewsPanelViewModel = value;
                OnPropertyChanged(nameof(UserReviewsPanelViewModel));
                UserReviewsPanelViewModel.PropertyChanged += UserReview_PropertyChanged;
            }
        }

        private RateFilmPanelViewModel _rateFilmPanelViewModel;

        public RateFilmPanelViewModel RateFilmPanelViewModel
        {
            get => _rateFilmPanelViewModel;
            set
            {
                _rateFilmPanelViewModel = value;
                OnPropertyChanged(nameof(RateFilmPanelViewModel));
                RateFilmPanelViewModel.PropertyChanged += UserReview_PropertyChanged;
            }
        }

        private FilmCastListViewModel _filmCastListViewModel;

        public FilmCastListViewModel FilmCastListViewModel
        {
            get => _filmCastListViewModel;
            set
            {
                _filmCastListViewModel = value;
                OnPropertyChanged(nameof(FilmCastListViewModel));
            }
        }

        private Film _film;

        public Film Film
        {
            get => _film;
            set
            {
                _film = value;
                OnPropertyChanged(nameof(Film));
            }
        }

        public double FilmAvgScore => FilmReviewsCount > 0 ? _unitOfWork.FilmReviewRepository.GetFilmAvgScore(Film.ID) : 0.0f;
        public int FilmReviewsCount => _unitOfWork.FilmReviewRepository.GetFilmReviewsCount(Film.ID);

        private FilmReview _currentUserFilmReview;

        public FilmReview CurrentUserFilmReview
        {
            get => _currentUserFilmReview;
            set
            {
                _currentUserFilmReview = value;
                OnPropertyChanged(nameof(CurrentUserFilmReview));
                OnPropertyChanged(nameof(CurrentUserScoredFilm));
                OnPropertyChanged(nameof(FilmReleased));
                OnPropertyChanged(nameof(FilmReviewsCount));
                OnPropertyChanged(nameof(FilmAvgScore));
            }
        }

        public bool CurrentUserScoredFilm => _currentUserFilmReview != null;
        public bool FilmReleased => Film?.ReleaseDate > DateTime.Now;

        public FilmViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private void UserReview_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserReviewsPanelViewModel.CurrentUserFilmReview) ||
                e.PropertyName == nameof(RateFilmPanelViewModel.CurrentUserFilmReview))
            {
                OnPropertyChanged(nameof(CurrentUserFilmReview));
                OnPropertyChanged(nameof(CurrentUserScoredFilm));
                OnPropertyChanged(nameof(FilmReviewsCount));
                OnPropertyChanged(nameof(FilmAvgScore));
            }
        }
    }
}