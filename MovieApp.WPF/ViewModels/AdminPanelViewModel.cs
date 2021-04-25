using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class AdminPanelViewModel : ViewModelBase
    {
        private readonly IStore<User> _usersStore;
        private readonly IStore<FilmReview> _filmReviewsStore;
        private readonly IStore<Film> _filmsStore;
        private readonly IStore<FilmCast> _filmCastsStore;
        private readonly IStore<Actor> _actorsStore;

        public List<User> Users => _usersStore.Entities;
        public List<FilmReview> Reviews => _filmReviewsStore.Entities;
        public List<Film> Films => _filmsStore.Entities;
        public List<FilmCast> Casts => _filmCastsStore.Entities;
        public List<Actor> Actors => _actorsStore.Entities;
        public AdminPanelViewModel(IStore<User> usersStore, IStore<FilmReview> filmReviewsStore, IStore<Film> filmsStore, IStore<FilmCast> filmCastsStore, IStore<Actor> actorsStore)
        {
            _usersStore = usersStore;
            _filmReviewsStore = filmReviewsStore;
            _filmsStore = filmsStore;
            _filmCastsStore = filmCastsStore;
            _actorsStore = actorsStore;
        }

    }
}
