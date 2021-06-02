using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.ViewModels
{
    public class UserReviewsPanelViewModel : ViewModelBase
    {
        private readonly ILeaveReviewService _leaveReviewService;
        public ICommand PublishReviewCommand { get; }
        public ICommand ChangeViewCommand { get; }

        public MessageViewModel InfoMessageViewModel { get; }

        private string _reviewText = string.Empty;

        private ObservableCollection<FilmReview> _reviews;

        public ObservableCollection<FilmReview> Reviews
        {
            get => _reviews;
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

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

        public string InfoMessage
        {
            set => InfoMessageViewModel.Message = value;
        }

        public UserReviewsPanelViewModel(IAuthenticator authentificator, ILeaveReviewService leaveReviewService, ICommand changeViewCommand)
        {
            _leaveReviewService = leaveReviewService;

            PublishReviewCommand = new PublishReviewCommand(this, authentificator, leaveReviewService);
            ChangeViewCommand = changeViewCommand;

            InfoMessageViewModel = new MessageViewModel();
        }
    }
}