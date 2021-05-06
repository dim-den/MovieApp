using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class UserReviewsPanelViewModel : ViewModelBase
    {
        public AsyncCommandBase PublishReviewCommand { get; }

        public MessageViewModel InfoMessageViewModel { get; }

        private string _reviewText = string.Empty;

        public ObservableCollection<FilmReview> Reviews { get; }

        public string ReviewText
        {
            get => _reviewText;
            set
            {
                _reviewText = value;
                OnPropertyChanged(nameof(ReviewText));
            }
        }

        private FilmReview _currentUserFilmReview;
        public FilmReview CurrentUserFilmReview
        {
            get => _currentUserFilmReview;
            set
            {
                _currentUserFilmReview = value;
                OnPropertyChanged(nameof(CurrentUserFilmReview));
            }
        }

        public Film Film { get; }

        public string InfoMessage
        {
            set => InfoMessageViewModel.Message = value;
        }

        public UserReviewsPanelViewModel(FilmReview currentUserFilmReview, Film film, IAuthenticator authentificator, IUnitOfWork unitOfWork, IStore<FilmReview> filmReviewStore)
        {
            _currentUserFilmReview = currentUserFilmReview;
            Film = film;
            Reviews = new ObservableCollection<FilmReview>(filmReviewStore.Entities);

            PublishReviewCommand = new PublishReviewCommand(this, authentificator, new LeaveReviewService(unitOfWork));

            InfoMessageViewModel = new MessageViewModel();
        }
    }
}
