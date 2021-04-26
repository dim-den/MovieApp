using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class FilmDataService : GenericDataService<Film>, IFilmDataService
    {
        public FilmDataService(MovieAppDbContext movieAppDbContext) : base(movieAppDbContext)
        {
        }
    }
}
