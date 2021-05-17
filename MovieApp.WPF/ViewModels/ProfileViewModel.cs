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

        private readonly IStore<FilmReview> _userFilmReviewsStore;

        public User User { get; }

        public bool HasReviews => _userFilmReviewsStore.Entities != null && _userFilmReviewsStore.Entities.Count > 0;

        public byte[] ImageData => User?.ImageData;

        public int FilmsWatched => (HasReviews == true) ? _userFilmReviewsStore.Entities.Count : 0;

        public double AvgScore => (HasReviews == true) ? _userFilmReviewsStore.Entities.Average(u => u.Score) : 0.0;

        public ProfileViewModel(INavigator navigator, IAuthenticator authenticator, User user, IStore<FilmReview> userFilmReviewsStore)
        {
            User = user;
            _userFilmReviewsStore = userFilmReviewsStore;

            UserRatingsViewModel = new UserRatingsViewModel(navigator, authenticator, userFilmReviewsStore);
        }
    }
}
