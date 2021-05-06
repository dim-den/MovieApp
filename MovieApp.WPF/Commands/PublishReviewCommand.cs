﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class PublishReviewCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaveReviewService _leaveReviewService;
        private readonly UserReviewsPanelViewModel _userReviewsViewModel;

        public override bool CanExecute(object parameter)
        {
            return _userReviewsViewModel.ReviewText.Length > 10 && _userReviewsViewModel.ReviewText.Length < 1000 && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            int score = Convert.ToInt32((double)parameter);

            string reviewText = _userReviewsViewModel.ReviewText;

            var review = await _leaveReviewService.LeaveReview(_userReviewsViewModel.Film,
                                                               _authenticator.CurrentUser,
                                                               reviewText,
                                                               score);

            if (_userReviewsViewModel.CurrentUserFilmReview == null)
            {
                _userReviewsViewModel.Reviews.Add(review);
                _userReviewsViewModel.InfoMessage = "Your review added!";
            }
            else
            {
                _userReviewsViewModel.InfoMessage = "Updated your existing review (refrash the page)";
            }

            _userReviewsViewModel.CurrentUserFilmReview = review;
        }

        public PublishReviewCommand(UserReviewsPanelViewModel userReviewsViewModel, IAuthenticator authenticator, ILeaveReviewService leaveReviewService)
        {
            _userReviewsViewModel = userReviewsViewModel;
            _authenticator = authenticator;
            _leaveReviewService = leaveReviewService;

            _userReviewsViewModel.PropertyChanged += FilmViewModel_PropertyChanged;
        }

        private void FilmViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserReviewsPanelViewModel.ReviewText))
            {
                OnCanExecuteChanged();
            }
        }
    }
}