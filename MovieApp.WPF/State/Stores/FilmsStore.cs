using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.FilmServices;
using MovieApp.EntityFramework.Services;

namespace MovieApp.WPF.State.Stores
{
    public class FilmsStore : IFilmStore
    {
        public List<Film> Films { get; set; }
        public FilmsStore(IFilmService filmService)
        {
            Films = new List<Film>(filmService.GetAllFilmsSync());
        }
    }
}
