using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MovieApp.WPF.ViewModels.Factories;

namespace MovieApp.WPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private ObservableCollection<FilmReview> _userFilmReviews;
        public ObservableCollection<FilmReview> UserFilmReviews
        {
            get => _userFilmReviews;
            set
            {
                _userFilmReviews = value;
                OnPropertyChanged(nameof(UserFilmReviews));
                OnPropertyChanged(nameof(HasReviews));
                OnPropertyChanged(nameof(FilmsWatched));
                OnPropertyChanged(nameof(AvgScore));
            }

        }

        private UserRatingsViewModel _userRatingsViewModel;
        public UserRatingsViewModel UserRatingsViewModel
        {
            get => _userRatingsViewModel;
            set
            {
                _userRatingsViewModel = value;
                OnPropertyChanged(nameof(UserRatingsViewModel));
            }
        }

        public ICommand ChangeViewCommand { get; }

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public bool HasReviews => UserFilmReviews != null && UserFilmReviews != null && UserFilmReviews.Count > 0;

        public byte[] ImageData => User?.ImageData;

        public int FilmsWatched => (HasReviews == true) ? UserFilmReviews.Count : 0;

        public double AvgScore => (HasReviews == true) ? UserFilmReviews.Average(u => u.Score) : 0.0;
        
        public ProfileViewModel(ICommand changeViewCommand)
        {
            ChangeViewCommand = changeViewCommand;
        }
    }
}
