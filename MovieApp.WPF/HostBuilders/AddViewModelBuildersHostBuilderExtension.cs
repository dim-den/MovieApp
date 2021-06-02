using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels.Builders;

namespace MovieApp.WPF.HostBuilders
{
    public static class AddViewModelBuildersHostBuilderExtension
    {
        public static IHostBuilder AddViewModelBuilders(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MovieCarouselViewModelBuilder>(services => CreateMovieCarouselViewModelBuilder(services));
                services.AddSingleton<ActorsSummaryViewModelBuilder>(services => CreateActorsSummaryViewModelBuilder(services));
                services.AddSingleton<UpcomingFilmsListViewModelBuilder>(services => CreateUpcomingFilmsListViewModelBuilder(services));
                services.AddSingleton<HomeViewModelBuilder>(services => CreateHomeViewModelBuilder(services));

                services.AddTransient<ProfileViewModelBuilder>(services => CreateProfileViewModelBuilder(services));

                services.AddTransient<ActorViewModelBuilder>(services => CreateActorViewModelBuilder(services));

                services.AddSingleton<RateFilmPanelViewModelBuilder>(services => CreateRateFilmPanelViewModelBuilder(services));
                services.AddSingleton<UserReviewsPanelViewModelBuilder>(services => CreateUserReviewsPanelViewModelBuilder(services));
                services.AddSingleton<FilmCastListViewModelBuilder>(services => CreateFilmCastListViewModelBuilder(services));
                services.AddSingleton<FilmViewModelBuilder>(services => CreateFilmViewModelBuilder(services));
            });

            return host;
        }

        private static MovieCarouselViewModelBuilder CreateMovieCarouselViewModelBuilder(IServiceProvider services)
        {
            return MovieCarouselViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                                      services.GetRequiredService<ChangeViewCommand>());
        }

        private static ActorsSummaryViewModelBuilder CreateActorsSummaryViewModelBuilder(IServiceProvider services)
        {
            return ActorsSummaryViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                                      services.GetRequiredService<ChangeViewCommand>());
        }
        
        private static UpcomingFilmsListViewModelBuilder CreateUpcomingFilmsListViewModelBuilder(IServiceProvider services)
        {
            return UpcomingFilmsListViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                                          services.GetRequiredService<ChangeViewCommand>());
        }

        private static HomeViewModelBuilder CreateHomeViewModelBuilder(IServiceProvider services)
        {
            return HomeViewModelBuilder.Init(services.GetRequiredService<MovieCarouselViewModelBuilder>(),
                                             services.GetRequiredService<ActorsSummaryViewModelBuilder>(),
                                             services.GetRequiredService<UpcomingFilmsListViewModelBuilder>());
        }

        private static ProfileViewModelBuilder CreateProfileViewModelBuilder(IServiceProvider services)
        {
            return ProfileViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                                services.GetRequiredService<ChangeViewCommand>());
        }
        
        private static ActorViewModelBuilder CreateActorViewModelBuilder(IServiceProvider services)
        {
            return ActorViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                              services.GetRequiredService<ChangeViewCommand>());
        }

        private static RateFilmPanelViewModelBuilder CreateRateFilmPanelViewModelBuilder(IServiceProvider services)
        {
            return RateFilmPanelViewModelBuilder.Init(services.GetRequiredService<IAuthenticator>(),
                                                      services.GetRequiredService<IUnitOfWork>(),
                                                      services.GetRequiredService<ILeaveReviewService>());
        }

        private static UserReviewsPanelViewModelBuilder CreateUserReviewsPanelViewModelBuilder(IServiceProvider services)
        {
            return UserReviewsPanelViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                                         services.GetRequiredService<IAuthenticator>(),
                                                         services.GetRequiredService<ILeaveReviewService>(),
                                                         services.GetRequiredService<ChangeViewCommand>());
        }

        private static FilmCastListViewModelBuilder CreateFilmCastListViewModelBuilder(IServiceProvider services)
        {
            return FilmCastListViewModelBuilder.Init(services.GetRequiredService<IUnitOfWork>(),
                                                     services.GetRequiredService<ChangeViewCommand>());
        }

        private static FilmViewModelBuilder CreateFilmViewModelBuilder(IServiceProvider services)
        {
            return FilmViewModelBuilder.Init(services.GetRequiredService<RateFilmPanelViewModelBuilder>(),
                                             services.GetRequiredService<UserReviewsPanelViewModelBuilder>(),
                                             services.GetRequiredService<FilmCastListViewModelBuilder>(),
                                             services.GetRequiredService<IUnitOfWork>(),
                                             services.GetRequiredService<IAuthenticator>());
        }
    }
}
