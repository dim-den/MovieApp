using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services
{
    public interface IFilmReviewDataService : IGenericDataService<FilmReview>
    {
        int GetFilmReviewsCount(int filmdID);

        double GetFilmAvgScore(int filmID);

        Task<FilmReview> GetUserFilmReview(int userID, int filmID);
    }
}
