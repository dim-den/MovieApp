using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }

        private readonly IStore<FilmCast> _actorFilmCastStore;

        public Actor Actor { get;  }

        public IOrderedEnumerable<FilmCast> ActorFilmCast => _actorFilmCastStore.Entities.OrderByDescending(u => u.Film.ReleaseDate);

        public bool HasFilmCast => ActorFilmCast != null && ActorFilmCast.Count() > 0;

        public string FilmingTime => $"{ActorFilmCast.Min(f => f.Film.ReleaseDate).Year} - {ActorFilmCast.Max(f => f.Film.ReleaseDate).Year}";

        public static double GetFilmRating(int filmID)
        {
            return (new UnitOfWork()).FilmReviewRepository.GetFilmAvgScore(filmID);
        }

        public ActorViewModel(INavigator navigator, IAuthenticator authentificator, Actor actor, IStore<FilmCast> actorFilmCastStore)
        {
            Actor = actor;
            _actorFilmCastStore = actorFilmCastStore;

            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);
        }
    }
}
