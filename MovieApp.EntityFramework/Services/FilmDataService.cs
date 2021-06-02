using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class FilmDataService : GenericDataService<Film>, IFilmDataService
    {
        public FilmDataService(MovieAppDbContext movieAppDbContext) : base(movieAppDbContext)
        {
        }

        public async Task<ICollection<Film>> GetUpcomingFilms()
        {
            return await DbSet.AsNoTracking().Where(f => f.ReleaseDate > DateTime.Now).OrderBy(d => d.ReleaseDate).ToListAsync();
        }

        public async Task<ICollection<Film>> GetRandomReleasedFilms(int randomEntitiesCount)
        {
            return await DbSet.AsNoTracking().Where(f => f.ReleaseDate <= DateTime.Now).OrderBy(r => Guid.NewGuid()).Take(randomEntitiesCount).ToListAsync();
        }
    }
}