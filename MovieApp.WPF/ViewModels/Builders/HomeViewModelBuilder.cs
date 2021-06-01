using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class HomeViewModelBuilder : IViewModelBuilder<HomeViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;

        private MovieCarouselViewModelBuilder _movieCarouselViewModelBuilder;
        private ActorsSummaryViewModelBuilder _actorsSummaryViewModelBuilder;
        private UpcomingFilmsListViewModelBuilder _upcomingFilmsListViewModelBuilder;

        private HomeViewModel _homeViewModel;

        private HomeViewModelBuilder(MovieCarouselViewModelBuilder movieCarouselViewModelBuilder,
                                     ActorsSummaryViewModelBuilder actorsSummaryViewModelBuilder,
                                     UpcomingFilmsListViewModelBuilder upcomingFilmsListViewModelBuilder)
        {
            _movieCarouselViewModelBuilder = movieCarouselViewModelBuilder;
            _actorsSummaryViewModelBuilder = actorsSummaryViewModelBuilder;
            _upcomingFilmsListViewModelBuilder = upcomingFilmsListViewModelBuilder;

            _homeViewModel = new HomeViewModel();
        }

        public HomeViewModelBuilder SetCarouselFilmsCount(int count)
        {
            _homeViewModel.MovieCarouselViewModel = _movieCarouselViewModelBuilder.SetFilmCount(count).Build();

            return this;
        }

        public HomeViewModelBuilder SetActorsCount(int count)
        {
            _homeViewModel.ActorsSummaryViewModel = _actorsSummaryViewModelBuilder.SetActorsCount(count).Build();

            return this;
        }

        public HomeViewModelBuilder SetUpcomingFilmsCount(int count)
        {
            _homeViewModel.UpcomingFilmsListViewModel = _upcomingFilmsListViewModelBuilder.SetFilmCount(count).Build();

            return this;
        }

        public static HomeViewModelBuilder Init(MovieCarouselViewModelBuilder movieCarouselViewModelBuilder,
                                                ActorsSummaryViewModelBuilder actorsSummaryViewModelBuilder,
                                                UpcomingFilmsListViewModelBuilder upcomingFilmsListViewModelBuilder)
        {
            return new HomeViewModelBuilder(movieCarouselViewModelBuilder,
                                            actorsSummaryViewModelBuilder,
                                            upcomingFilmsListViewModelBuilder);
        }

        public HomeViewModel Build()
        {
            return _homeViewModel;
        }
    }
}
