using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.ReviewServices
{
    public interface ILeaveReviewService
    {
        Task<FilmReview> LeaveReview(Film film, User user, string reviewText, int score);

        Task<FilmReview> LeaveScore(Film film, User user, int score);
    }
}
