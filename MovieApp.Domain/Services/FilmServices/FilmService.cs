using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.FilmServices
{
    public class FilmService : IFilmService
    {
        private readonly IFilmDataService _filmDataService; 
        public async Task<ICollection<Film>> GetAllFilms()
        {
            return await _filmDataService.GetAll();
        }

        public ICollection<Film> GetAllFilmsSync()
        {
            return _filmDataService.GetAllSync();
        }

        public FilmService(IFilmDataService filmDataService)
        {
            _filmDataService = filmDataService;
        }
    }
}
