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
    }
}
