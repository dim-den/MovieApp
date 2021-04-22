using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MovieCarouselViewModel MovieCarouselViewModel { get; }
        public ActorsSummaryViewModel ActorsSummaryViewModel { get; }

        private readonly INavigator _navigator;

        private readonly IStore<Film> _filmStore;
        private readonly IStore<Actor> _actorStore;
        public HomeViewModel(INavigator navigator, IStore<Film> filmStore, IStore<Actor> actorStore)
        {
            _navigator = navigator;

            _filmStore = filmStore;
            _actorStore = actorStore;

            MovieCarouselViewModel = new MovieCarouselViewModel(_navigator, _filmStore);
            ActorsSummaryViewModel = new ActorsSummaryViewModel(_navigator, _actorStore);
        }
    }
}
