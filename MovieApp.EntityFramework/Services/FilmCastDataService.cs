using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class FilmCastDataService : GenericDataService<FilmCast>, IFilmCastDataService
    {
        public FilmCastDataService(MovieAppDbContext movieAppDbContext) : base(movieAppDbContext)
        {
        }
    }
}