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
        private MovieCarouselViewModel _movieCarouselViewModel;
        public MovieCarouselViewModel MovieCarouselViewModel
        {
            get => _movieCarouselViewModel;
            set
            {
                _movieCarouselViewModel = value;
                OnPropertyChanged(nameof(MovieCarouselViewModel));
            }
        }

        private ActorsSummaryViewModel _actorsSummaryViewModel;
        public ActorsSummaryViewModel ActorsSummaryViewModel
        {
            get => _actorsSummaryViewModel;
            set
            {
                _actorsSummaryViewModel = value;
                OnPropertyChanged(nameof(ActorsSummaryViewModel));
            }
        }

        private UpcomingFilmsListViewModel _upcomingFilmsListViewModel;
        public UpcomingFilmsListViewModel UpcomingFilmsListViewModel
        {
            get => _upcomingFilmsListViewModel;
            set
            {
                _upcomingFilmsListViewModel = value;
                OnPropertyChanged(nameof(UpcomingFilmsListViewModel));
            }
        }

        public HomeViewModel()
        {
        }
    }
}
