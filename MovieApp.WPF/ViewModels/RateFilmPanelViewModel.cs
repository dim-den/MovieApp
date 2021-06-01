using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private readonly IAuthenticator _authentificator;
        public ICommand RateFilmCommand { get; }

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

        public double FilmAvgScore => Film != null && FilmReviewsCount > 0 ? _unitOfWork.FilmReviewRepository.GetFilmAvgScore(Film.ID) : 0.0f;
        public int FilmReviewsCount => Film != null ? _unitOfWork.FilmReviewRepository.GetFilmReviewsCount(Film.ID) : 0;


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

        public RateFilmPanelViewModel(IAuthenticator authentificator, IUnitOfWork unitOfWork, ILeaveReviewService leaveReviewService)
        {
            _unitOfWork = unitOfWork;

            RateFilmCommand = new UserRateFilmCommand(this, authentificator, leaveReviewService);
        }
    }
}
