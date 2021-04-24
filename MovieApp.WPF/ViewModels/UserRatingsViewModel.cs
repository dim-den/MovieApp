using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class UserRatingsViewModel : ViewModelBase
    {
        public AsyncCommandBase FilterReviewsCommand { get; }

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
        public UserRatingsViewModel(IStore<FilmReview> filmReviewsStore)
        {
            FilterReviewsCommand = new UserReviewsFilterCommand(this, filmReviewsStore);

            _userReviews = new ObservableCollection<FilmReview>(filmReviewsStore.Entities);
        }
    }
}
