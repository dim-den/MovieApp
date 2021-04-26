using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Services
{
    public interface IUnitOfWork : IDisposable
    {
       IUserDataService UserRepository { get;  }
       IFilmReviewDataService FilmReviewRepository { get;  }
       IFilmDataService FilmRepository { get;  }
       IFilmCastDataService FilmCastRepository { get; }
       IActorDataService ActorRepository { get;  }

        int Save();
        Task<int> SaveAsync();

    }
}
