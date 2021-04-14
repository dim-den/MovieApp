using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Services.FilmServices;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MovieCarouselViewModel MovieCarouselViewModel { get; }
        private readonly INavigator _navigator;
        private readonly IFilmStore _filmStore;
        public HomeViewModel(INavigator navigator)
        {
            _navigator = navigator;
            _filmStore = new FilmsStore(new FilmService(new FilmDataService()));
            MovieCarouselViewModel = new MovieCarouselViewModel(_navigator, _filmStore);
        }

    }
}
