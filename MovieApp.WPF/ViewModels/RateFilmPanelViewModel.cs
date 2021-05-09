using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class RateFilmPanelViewModel : ViewModelBase 
    {
        private readonly IUnitOfWork _unitOfWork;
        public AsyncCommandBase RateFilmCommand { get; }

        private FilmReview _currentUserFilmReview;
        public FilmReview CurrentUserFilmReview
        {
            get => _currentUserFilmReview;
            set
            {
                _currentUserFilmReview = value;
                OnPropertyChanged(nameof(CurrentUserFilmReview));
                OnPropertyChanged(nameof(FilmAvgScore));
                OnPropertyChanged(nameof(FilmReviewsCount));
            }
        }
        public double FilmAvgScore => FilmReviewsCount > 0 ? _unitOfWork.FilmReviewRepository.GetFilmAvgScore(Film.ID) : 0.0f;

        public int FilmReviewsCount => _unitOfWork.FilmReviewRepository.GetFilmReviewsCount(Film.ID);

        public Film Film { get; }

        public RateFilmPanelViewModel(FilmReview currentUserFilmReview, Film film, IAuthenticator authentificator, IUnitOfWork unitOfWork)
        {
            _currentUserFilmReview = currentUserFilmReview;
            _unitOfWork = unitOfWork;
            Film = film;

            RateFilmCommand = new UserRateFilmCommand(this, authentificator, new LeaveReviewService(unitOfWork));
        }
    }
}
