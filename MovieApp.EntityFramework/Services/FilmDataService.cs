using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework.Services.Common;

namespace MovieApp.EntityFramework.Services
{
    public class FilmDataService : IFilmDataService
    {
        private readonly NonQueryDataService<Film> _nonQueryDataService;

        public FilmDataService()
        {
            _nonQueryDataService = new NonQueryDataService<Film>();
        }

        public async Task<Film> Create(Film entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Film> Get(int id)
        {
            using MovieAppDbContext context = new();
            return await context.Films
                .Include(u => u.FilmReviews)
                .Include(u => u.FilmCasts)
                .FirstOrDefaultAsync((e) => e.ID == id);
        }

        public async Task<ICollection<Film>> GetAll()
        {
            using MovieAppDbContext context = new();
            return await context.Films
                .Include(u => u.FilmReviews)
                .Include(u => u.FilmCasts)
                .ToListAsync();
        }

        public ICollection<Film> GetAllSync()
        {
            using MovieAppDbContext context = new();
            return context.Films
                .Include(u => u.FilmReviews)
                .Include(u => u.FilmCasts)
                .ToList();
        }

        public async Task<Film> Update(int id, Film entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
