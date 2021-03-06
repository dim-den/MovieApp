using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public enum FilterReviewType
    {
        ScoreOne = 1,
        ScoreTwo,
        ScoreThree,
        ScoreFour,
        ScoreFive,
        ScoreSix,
        ScoreSeven,
        ScoreEight,
        ScoreNine,
        ScoreTen,
        SelectAll,
        SortByDate,
        SortByScore,
        SortByTitle,
        DateBetween
    }

    public class UserReviewsFilterCommand : AsyncCommandBase
    {
        private readonly ObservableCollection<FilmReview> _filmReviews;
        private readonly UserRatingsViewModel _userRatingsViewModel;

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            FilterReviewType filterReviewType = (FilterReviewType)parameter;

            if ((int)filterReviewType >= 1 && (int)filterReviewType <= 10)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviews
                .Where(review => review.Score == (int)filterReviewType));
            }
            else if (filterReviewType == FilterReviewType.SortByDate)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviews.OrderByDescending(review => review.Date));
            }
            else if (filterReviewType == FilterReviewType.SortByScore)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviews.OrderByDescending(review => review.Score));
            }
            else if (filterReviewType == FilterReviewType.SortByTitle)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviews.OrderBy(review => review.Film.Title));
            }
            else if (filterReviewType == FilterReviewType.DateBetween)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviews
                .Where(review => review.Date.Date <= _userRatingsViewModel.ToDate.Value.Date &&
                                 review.Date.Date >= _userRatingsViewModel.FromDate.Value.Date));
            }
            else
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviews);
            }
        }

        public UserReviewsFilterCommand(UserRatingsViewModel userRatingsViewModel, ObservableCollection<FilmReview> filmReviews)
        {
            _userRatingsViewModel = userRatingsViewModel;
            _filmReviews = filmReviews;
        }
    }
}