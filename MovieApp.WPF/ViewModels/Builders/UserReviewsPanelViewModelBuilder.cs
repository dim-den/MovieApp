using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class UserReviewsPanelViewModelBuilder : IViewModelBuilder<UserReviewsPanelViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserReviewsPanelViewModel _userReviewsPanelViewModel;

        private UserReviewsPanelViewModelBuilder(IUnitOfWork unitOfWork, IAuthenticator authenticator, ILeaveReviewService leaveReviewService, ICommand changeViewCommand)
        {
            _userReviewsPanelViewModel = new UserReviewsPanelViewModel(authenticator, leaveReviewService, changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public UserReviewsPanelViewModelBuilder SetFilm(Film film)
        {
            _userReviewsPanelViewModel.Film = film;
            LoadReviews();

            return this;
        }

        public static UserReviewsPanelViewModelBuilder Init(IUnitOfWork unitOfWork, IAuthenticator authenticator, ILeaveReviewService leaveReviewService, ICommand changeViewCommand)
        {
            return new UserReviewsPanelViewModelBuilder(unitOfWork, authenticator, leaveReviewService, changeViewCommand);
        }

        public UserReviewsPanelViewModel Build()
        {
            return _userReviewsPanelViewModel;
        }

        private void LoadReviews()
        {
            _unitOfWork.FilmReviewRepository.GetWithInclude(r => r.FilmID == _userReviewsPanelViewModel.Film.ID && !string.IsNullOrEmpty(r.ReviewText), r => r.User).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _userReviewsPanelViewModel.Reviews = new ObservableCollection<FilmReview>(task.Result);
                }
            });
        }
    }
}