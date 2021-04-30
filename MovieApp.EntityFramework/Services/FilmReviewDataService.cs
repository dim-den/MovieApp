using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class FilmReviewDataService : GenericDataService<FilmReview>, IFilmReviewDataService
    {
        public FilmReviewDataService(MovieAppDbContext movieAppDbContext) : base(movieAppDbContext)
        {
        }

        public int GetFilmReviewsCount(int filmdID)
        {
            return DbSet.Count(r => r.FilmID == filmdID);
        }

        public double GetFilmAvgScore(int filmID)
        {
            return DbSet.Where(r => r.FilmID == filmID).Average(r => r.Score);
        }
    }
}
