using System;
using System.Threading.Tasks;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework.Services;

namespace MovieApp.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        private readonly MovieAppDbContext _movieAppDbContext;

        private IUserDataService _userDataService;
        private IFilmReviewDataService _filmReviewDataService;
        private IFilmDataService _filmDataService;
        private IFilmCastDataService _filmCastDataService;
        private IActorDataService _actorDataService;

        public IUserDataService UserRepository => _userDataService ?? (_userDataService = new UserDataService(_movieAppDbContext));
        public IFilmReviewDataService FilmReviewRepository => _filmReviewDataService ?? (_filmReviewDataService = new FilmReviewDataService(_movieAppDbContext));
        public IFilmDataService FilmRepository => _filmDataService ?? (_filmDataService = new FilmDataService(_movieAppDbContext));
        public IFilmCastDataService FilmCastRepository => _filmCastDataService ?? (_filmCastDataService = new FilmCastDataService(_movieAppDbContext));
        public IActorDataService ActorRepository => _actorDataService ?? (_actorDataService = new ActorDataService(_movieAppDbContext));

        public UnitOfWork(MovieAppDbContextFactory movieAppDbContextFactory)
        {
            _movieAppDbContext = movieAppDbContextFactory.CreateDbContext();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _movieAppDbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public int Save()
        {
            return _movieAppDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _movieAppDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}