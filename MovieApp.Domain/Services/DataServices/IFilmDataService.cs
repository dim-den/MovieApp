using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services
{
    public interface IFilmDataService : IGenericDataService<Film>
    {
        Task<ICollection<Film>> GetUpcomingFilms();

        Task<ICollection<Film>> GetRandomReleasedFilms(int randomEntitiesCount);
    }
}