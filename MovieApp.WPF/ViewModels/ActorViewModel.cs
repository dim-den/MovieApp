using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorViewModel : ViewModelBase
    {
        private readonly IStore<FilmCast> _actorFilmCastStore;
        public Actor Actor { get;  }

        public IOrderedEnumerable<FilmCast> ActorFilmCast => _actorFilmCastStore.Entities.OrderByDescending(u => u.Film.ReleaseDate);

        public string FilmingTime => $"{ActorFilmCast.Min(f => f.Film.ReleaseDate).Year} - {ActorFilmCast.Max(f => f.Film.ReleaseDate).Year}";

        public double GetFilmRating(int filmID)
        {
            return (new UnitOfWork()).FilmReviewRepository.GetFilmAvgScore(filmID);
        }

        public ActorViewModel(Actor actor, IStore<FilmCast> actorFilmCastStore)
        {
            Actor = actor;
            _actorFilmCastStore = actorFilmCastStore;
        }
    }
}
