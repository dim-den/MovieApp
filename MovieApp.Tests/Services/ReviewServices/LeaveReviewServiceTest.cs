using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using NUnit.Framework;

namespace MovieApp.Tests.Services.ReviewServices
{
    [TestFixture]
    public class LeaveReviewServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private LeaveReviewService _leaveReviewService;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _leaveReviewService = new LeaveReviewService(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task LeaveReview_WithNotExistingUserReview_ReturnsNewFilmReview()
        {
            User user = CreateUser();
            Film film = CreateFilm();
            string reviewText = "test review text";
            int score = 5;
            _mockUnitOfWork.Setup(s => s.FilmReviewRepository.Create(It.IsAny<FilmReview>()))
                .ReturnsAsync(CreateFilmReview(user, film, reviewText, score));

            FilmReview filmReview = await _leaveReviewService.LeaveReview(film, user, reviewText, score);

            Assert.AreEqual(reviewText, filmReview.ReviewText);
            Assert.AreEqual(score, filmReview.Score);
        }

        [Test]
        public async Task LeaveReview_WithExistingUserReview_ReturnsUpdatedFilmReview()
        {
            User user = CreateUser();
            Film film = CreateFilm();
            string reviewText = "test review text";
            int score = 5;

            string updatedReviewText = "updated test review text";
            int updatedScore = 8;

            FilmReview existingFilmReview = CreateFilmReview(user, film, reviewText, score);
            _mockUnitOfWork.Setup(s => s.FilmReviewRepository.GetUserFilmReview(user.ID, film.ID)).ReturnsAsync(existingFilmReview);
            _mockUnitOfWork.Setup(s => s.FilmReviewRepository.Update(It.IsAny<int>(), existingFilmReview))
                .ReturnsAsync(CreateFilmReview(user, film, updatedReviewText, updatedScore));

            FilmReview filmReview = await _leaveReviewService.LeaveReview(film, user, updatedReviewText, updatedScore);

            Assert.AreEqual(updatedScore, filmReview.Score);
            Assert.AreEqual(updatedReviewText, filmReview.ReviewText);
        }

        [Test]
        public async Task LeaveScore_WithNotExistingUserScore_ReturnsNewFilmReview()
        {
            User user = CreateUser();
            Film film = CreateFilm();
            int score = 4;
            _mockUnitOfWork.Setup(s => s.FilmReviewRepository.Create(It.IsAny<FilmReview>()))
                .ReturnsAsync(CreateFilmReview(user, film, It.IsAny<string>(), score));

            FilmReview filmReview = await _leaveReviewService.LeaveScore(film, user, score);

            Assert.AreEqual(score, filmReview.Score);
        }

        [Test] //LeaveReview_WithExistingUserReview_ReturnsUpdatedFilmReview
        public async Task LeaveScore_WithExistingUserScore_ReturnsUpdatedFilmScore()
        {

            User user = CreateUser();
            Film film = CreateFilm();
            int score = 5;

            int updatedScore = 8;

            FilmReview existingFilmReview = CreateFilmReview(user, film, It.IsAny<string>(), score);
            _mockUnitOfWork.Setup(s => s.FilmReviewRepository.GetUserFilmReview(user.ID, film.ID)).ReturnsAsync(existingFilmReview);
            _mockUnitOfWork.Setup(s => s.FilmReviewRepository.Update(It.IsAny<int>(), existingFilmReview))
                .ReturnsAsync(CreateFilmReview(user, film, It.IsAny<string>(), updatedScore));

            FilmReview filmReview = await _leaveReviewService.LeaveScore(film, user, updatedScore);

            Assert.AreEqual(updatedScore, filmReview.Score);
        }

        private Film CreateFilm()
        {
            return new Film()
            {
                ID = It.IsAny<int>()
            };
        }

        private User CreateUser()
        {
            return new User()
            {
                ID = It.IsAny<int>()
            };
        }

        private FilmReview CreateFilmReview(User user, Film film, string reviewText, int score)
        {
            return new FilmReview()
            {
                User = user,
                Film = film,
                ReviewText = reviewText,
                Score = score
            };
        }

    }
}
