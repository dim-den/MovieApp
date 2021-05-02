using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class FilmViewModel : ViewModelBase
    {
        public AsyncCommandBase RateFilmCommand { get; }
        public AsyncCommandBase PublishReviewCommand { get; }

        public MessageViewModel InfoMessageViewModel { get; }

        private readonly INavigator _navigator;
        private readonly IAuthenticator _authentificator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStore<FilmCast> _filmCastStore;
        private readonly IStore<FilmReview> _filmReviewStore;
        private string _reviewText = string.Empty;

        public string ReviewText
        {
            get => _reviewText;
            set
            {
                _reviewText = value;
                OnPropertyChanged(nameof(ReviewText));
            }
        }
        public Film Film { get;  }

        public double FilmAvgScore => _unitOfWork.FilmReviewRepository.GetFilmAvgScore(Film.ID);

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
                OnPropertyChanged(nameof(FilmReviewsCount));
                OnPropertyChanged(nameof(FilmAvgScore));
            }
        }

        public string InfoMessage
        {
            set => InfoMessageViewModel.Message = value;
        }

        public bool CurrentUserScoredFilm => _currentUserFilmReview != null;

        public List<FilmCast> Cast => _filmCastStore.Entities;

        public List<FilmReview> Reviews => _filmReviewStore.Entities;

        public FilmViewModel(INavigator navigator, IAuthenticator authentificator, 
                             IStore<FilmCast> filmCastStore, IStore<FilmReview> filmReviewStore,  Film film, FilmReview CurrentUserFilmReview)
        {
            _unitOfWork = new UnitOfWork();
            _navigator = navigator;
            _authentificator = authentificator;
            _filmCastStore = filmCastStore;
            _filmReviewStore = filmReviewStore;
            _currentUserFilmReview = CurrentUserFilmReview;

            RateFilmCommand = new UserRateFilmCommand(this, _unitOfWork, _authentificator);
            PublishReviewCommand = new PublishReviewCommand(this, _unitOfWork, _authentificator);

            InfoMessageViewModel = new MessageViewModel();

            Film = film;
        }
    }
}
