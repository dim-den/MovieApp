using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<FilmReview> GetUserFilmReview(int userID, int filmID)
        {
            return await DbSet.FirstOrDefaultAsync(r => r.UserID == userID && r.FilmID == filmID);
        }
    }
}