using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class PublishReviewCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FilmViewModel _filmViewModel;

        public override bool CanExecute(object parameter)
        {
            return _filmViewModel.ReviewText.Length > 10 && _filmViewModel.ReviewText.Length < 1000 && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            int score = Convert.ToInt32((double)parameter);

            string reviewText = _filmViewModel.ReviewText;

            var review = _filmViewModel.CurrentUserFilmReview;

            if (review == null)
            {
                var newReview = new FilmReview()
                {
                    FilmID = _filmViewModel.Film.ID,
                    UserID = _authenticator.CurrentUser.ID,
                    ReviewText = reviewText,
                    Score = score,
                    Date = DateTime.Now
                };

                _filmViewModel.CurrentUserFilmReview = await _unitOfWork.FilmReviewRepository.Create(newReview);
                _filmViewModel.InfoMessage = "Your review added!";
            }
            else
            {
                review.Score = score;
                review.ReviewText = reviewText;

                _filmViewModel.CurrentUserFilmReview = await _unitOfWork.FilmReviewRepository.Update(review.ID, review);
                _filmViewModel.InfoMessage = "Updated your existing review";
            }

            await _unitOfWork.SaveAsync();
        }

        public PublishReviewCommand(FilmViewModel filmViewModel, IUnitOfWork unitOfWork, IAuthenticator authenticator)
        {
            _filmViewModel = filmViewModel;
            _unitOfWork = unitOfWork;
            _authenticator = authenticator;

            _filmViewModel.PropertyChanged += FilmViewModel_PropertyChanged;
        }

        private void FilmViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FilmViewModel.ReviewText))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
