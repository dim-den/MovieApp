using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class ProfileViewModelBuilder : IViewModelBuilder<ProfileViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private ProfileViewModel _profileViewModel;

        private ProfileViewModelBuilder(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _profileViewModel = new ProfileViewModel(changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public ProfileViewModelBuilder SetUser(User user)
        {
            _profileViewModel.User = user;
            LoadUserReviews();

            return this;
        }

        public static ProfileViewModelBuilder Init(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            return new ProfileViewModelBuilder(unitOfWork, changeViewCommand);
        }

        public ProfileViewModel Build()
        {
            return _profileViewModel;
        }

        private void LoadUserReviews()
        {
            _unitOfWork.FilmReviewRepository.GetWithInclude(f => f.UserID == _profileViewModel.User.ID, f => f.Film).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _profileViewModel.UserFilmReviews = new ObservableCollection<FilmReview>(task.Result);
                    _profileViewModel.UserRatingsViewModel = new UserRatingsViewModel(_profileViewModel.UserFilmReviews, _profileViewModel.ChangeViewCommand);
                }
            });
        }
    }
}