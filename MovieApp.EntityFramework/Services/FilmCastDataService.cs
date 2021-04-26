using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
