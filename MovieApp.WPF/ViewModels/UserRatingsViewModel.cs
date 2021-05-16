using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class UserRatingsViewModel : ViewModelBase
    {
        public ICommand FilterReviewsCommand { get; }
        public ICommand ChangeViewCommand { get; }

        private ObservableCollection<FilmReview> _userReviews;
        public ObservableCollection<FilmReview> UserReviews
        {
            get
            {
                return _userReviews;
            }
            set
            {
                _userReviews = value;
                OnPropertyChanged(nameof(UserReviews));
                OnPropertyChanged(nameof(HasReviews));
            }
        }

        public bool HasReviews => UserReviews.Count > 0;

        public bool CanUserFilterByDate => _fromDate != null && _toDate != null && _fromDate <= _toDate;

        private DateTime? _fromDate;
        private DateTime? _toDate;

        public DateTime? FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                OnPropertyChanged(nameof(FromDate));
                OnPropertyChanged(nameof(CanUserFilterByDate));
            }
        }
        public DateTime? ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                OnPropertyChanged(nameof(ToDate));
                OnPropertyChanged(nameof(CanUserFilterByDate));
            }
        }
        public UserRatingsViewModel(INavigator navigator, IAuthenticator authentificator, IStore<FilmReview> filmReviewsStore)
        {
            FilterReviewsCommand = new UserReviewsFilterCommand(this, filmReviewsStore);
            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);

            _userReviews = new ObservableCollection<FilmReview>(filmReviewsStore.Entities);
        }
    }
}
