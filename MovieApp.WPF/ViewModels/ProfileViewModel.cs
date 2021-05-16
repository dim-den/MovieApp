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

        private readonly IUnitOfWork _unitOfWork;

        public User User { get; }

        public bool HasReviews => User != null && User.FilmReviews != null && User.FilmReviews.Count > 0;

        public byte[] ImageData => User?.ImageData;

        public int FilmsWatched => (HasReviews == true) ? User.FilmReviews.Count : 0;

        public double AvgScore => (HasReviews == true) ? User.FilmReviews.Average(u => u.Score) : 0.0;

        public ProfileViewModel(INavigator navigator, IAuthenticator authenticator, User user, IStore<FilmReview> userFilmReviewsStore)
        {
            User = user;

            UserRatingsViewModel = new UserRatingsViewModel(navigator, authenticator, userFilmReviewsStore);
        }
    }
}
