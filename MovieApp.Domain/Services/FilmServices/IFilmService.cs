using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.FilmServices
{
    public interface IFilmService
    {
        Task<ICollection<Film>> GetAllFilms();
        ICollection<Film> GetAllFilmsSync();
    }
}
