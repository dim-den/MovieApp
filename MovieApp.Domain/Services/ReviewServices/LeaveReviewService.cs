using System;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.ReviewServices
{
    public class LeaveReviewService : ILeaveReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FilmReview> LeaveReview(Film film, User user, string reviewText, int score)
        {
            FilmReview userFilmReview = await _unitOfWork.FilmReviewRepository.GetUserFilmReview(user.ID, film.ID);

            if (userFilmReview == null)
            {
                userFilmReview = new FilmReview()
                {
                    FilmID = film.ID,
                    UserID = user.ID,
                    ReviewText = reviewText,
                    Score = score,
                    Date = DateTime.Now
                };

                userFilmReview = await _unitOfWork.FilmReviewRepository.Create(userFilmReview);
            }
            else
            {
                userFilmReview.Score = score;
                userFilmReview.ReviewText = reviewText;

                userFilmReview = await _unitOfWork.FilmReviewRepository.Update(userFilmReview.ID, userFilmReview);
            }

            await _unitOfWork.SaveAsync();
            return userFilmReview;
        }

        public async Task<FilmReview> LeaveScore(Film film, User user, int score)
        {
            FilmReview userFilmReview = await _unitOfWork.FilmReviewRepository.GetUserFilmReview(user.ID, film.ID);

            if (userFilmReview == null)
            {
                userFilmReview = new FilmReview()
                {
                    FilmID = film.ID,
                    UserID = user.ID,
                    Score = score,
                    Date = DateTime.Now
                };

                userFilmReview = await _unitOfWork.FilmReviewRepository.Create(userFilmReview);
            }
            else
            {
                userFilmReview.Score = score;

                userFilmReview = await _unitOfWork.FilmReviewRepository.Update(userFilmReview.ID, userFilmReview);
            }

            await _unitOfWork.SaveAsync();
            return userFilmReview;
        }
    }
}