using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MovieApp.Domain.Models;
using MovieApp.WPF.State.Stores;
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

        private readonly IStore<FilmReview> _filmReviewsStore;
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
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviewsStore.Entities
                .Where(review => review.Score == (int)filterReviewType));
            }
            else if (filterReviewType == FilterReviewType.SortByDate)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviewsStore.Entities.OrderByDescending(review => review.Date));
            }
            else if (filterReviewType == FilterReviewType.SortByScore)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviewsStore.Entities.OrderByDescending(review => review.Score));
            }
            else if (filterReviewType == FilterReviewType.SortByTitle)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviewsStore.Entities.OrderBy(review => review.Film.Title));            
            }
            else if (filterReviewType == FilterReviewType.DateBetween)
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviewsStore.Entities
                .Where(review => review.Date.Date <= _userRatingsViewModel.ToDate.Value.Date &&
                                 review.Date.Date >= _userRatingsViewModel.FromDate.Value.Date));
            }
            else
            {
                _userRatingsViewModel.UserReviews = new ObservableCollection<FilmReview>(_filmReviewsStore.Entities);
            }
        }

        public UserReviewsFilterCommand(UserRatingsViewModel userRatingsViewModel, IStore<FilmReview> filmReviewsStore)
        {
            _userRatingsViewModel = userRatingsViewModel;
            _filmReviewsStore = filmReviewsStore;
        }

    }
}

