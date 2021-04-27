using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public UserRatingsViewModel UserRatingsViewModel { get; }
        public AsyncCommandBase ChangeImageCommand { get; set; }
        
        private readonly IAuthenticator _authentificator;
        private readonly IUnitOfWork _unitOfWork;

        public User CurrentUser => _authentificator.CurrentUser;

        public bool HasReviews => CurrentUser != null && CurrentUser?.FilmReviews != null && CurrentUser.FilmReviews.Count > 0;

        public byte[] ImageData => CurrentUser?.ImageData;

        public int FilmsWatched => (HasReviews == true) ? CurrentUser.FilmReviews.Count : 0;

        public double AvgScore => (HasReviews == true) ? CurrentUser.FilmReviews.Average(u => u.Score) : 0.0;

        public ProfileViewModel(INavigator navigator, IAuthenticator authentificator, IStore<FilmReview> userFilmReviewsStore)
        {
            _authentificator = authentificator;
            _unitOfWork = new UnitOfWork();

            UserRatingsViewModel = new UserRatingsViewModel(userFilmReviewsStore);

            ChangeImageCommand = new ChangeImageCommand(_authentificator, _unitOfWork);

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(ImageData));
        }
    }
}
