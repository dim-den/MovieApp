using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MovieCarouselViewModel MovieCarouselViewModel { get; }

        public ActorsSummaryViewModel ActorsSummaryViewModel { get; }

        public UpcomingFilmsListViewModel UpcomingFilmsListViewModel { get; }

        public HomeViewModel( MovieCarouselViewModel movieCarouselViewModel, ActorsSummaryViewModel actorsSummaryViewModel, UpcomingFilmsListViewModel upcomingFilmsListViewModel)
        {
            MovieCarouselViewModel = movieCarouselViewModel;
            ActorsSummaryViewModel = actorsSummaryViewModel;
            UpcomingFilmsListViewModel = upcomingFilmsListViewModel;
        }
    }
}
