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
    public class UserRateFilmCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FilmViewModel _filmViewModel;

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _filmViewModel.InfoMessage = string.Empty;

            int score = Convert.ToInt32((string) parameter);

            var review = _filmViewModel.CurrentUserFilmReview;

            if(review == null)
            {
                var newReview = new FilmReview()
                { 
                    FilmID = _filmViewModel.Film.ID,
                    UserID = _authenticator.CurrentUser.ID,
                    Score = score,
                    Date = DateTime.Now
                };

                _filmViewModel.CurrentUserFilmReview = await _unitOfWork.FilmReviewRepository.Create(newReview);
            }
            else
            {
                review.Score = score;

                _filmViewModel.CurrentUserFilmReview = await _unitOfWork.FilmReviewRepository.Update(review.ID, review);
            }

            await _unitOfWork.SaveAsync();
        }

        public UserRateFilmCommand(FilmViewModel filmViewModel, IUnitOfWork unitOfWork, IAuthenticator authenticator)
        {
            _filmViewModel = filmViewModel;
            _unitOfWork = unitOfWork;
            _authenticator = authenticator;
        }
    }
}
