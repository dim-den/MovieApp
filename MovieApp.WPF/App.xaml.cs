using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.EntityFramework;
using MovieApp.WPF.ViewModels;
using MovieApp.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels.Factories;
using MovieApp.WPF.Commands;
using MovieApp.Domain.Models;
using MovieApp.WPF.ViewModels.Builders;

namespace MovieApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var vCulture = new CultureInfo("en-EN");

            Thread.CurrentThread.CurrentCulture = vCulture;
            Thread.CurrentThread.CurrentUICulture = vCulture;
            CultureInfo.DefaultThreadCurrentCulture = vCulture;
            CultureInfo.DefaultThreadCurrentUICulture = vCulture;

            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IUserDataService, UserDataService>();
            services.AddSingleton<IFilmReviewDataService, FilmReviewDataService>();
            services.AddSingleton<IFilmDataService, FilmDataService>();
            services.AddSingleton<IFilmCastDataService, FilmCastDataService>();
            services.AddSingleton<IActorDataService, ActorDataService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILeaveReviewService, LeaveReviewService>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddScoped<Account>();
            services.AddScoped<INavigator, Navigator>();        
            services.AddScoped<IAuthenticator, Authenticator>();

            services.AddSingleton<ChangeViewCommand>();

            services.AddSingleton<HomeViewModel>(services => new HomeViewModel(
                   MovieCarouselViewModel.LoadMovieCarouselViewModel(services.GetRequiredService<IUnitOfWork>(), services.GetRequiredService<ChangeViewCommand>()),
                   ActorsSummaryViewModel.LoadActorsSummaryViewModel(services.GetRequiredService<IUnitOfWork>(), services.GetRequiredService<ChangeViewCommand>()),
                   UpcomingFilmsListViewModel.LoadUpcomingFilmsListViewModel(services.GetRequiredService<IUnitOfWork>(), services.GetRequiredService<ChangeViewCommand>())
                   ));

            services.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
            {
                return () => services.GetRequiredService<HomeViewModel>();
            });

            services.AddSingleton<Renavigator<HomeViewModel>>();

            services.AddSingleton<LoginViewModel>(services => new LoginViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<ChangeViewCommand>(),
                    services.GetRequiredService<Renavigator<HomeViewModel>>())
                  );

            services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => services.GetRequiredService<LoginViewModel>();
            });

            services.AddSingleton<Renavigator<LoginViewModel>>();

            services.AddSingleton<RegisterViewModel>(services => new RegisterViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<ChangeViewCommand>(),
                    services.GetRequiredService<Renavigator<HomeViewModel>>())
                  );

            services.AddSingleton<CreateViewModel<RegisterViewModel>>(services =>
            {
                return () => services.GetRequiredService<RegisterViewModel>();
            });

            services.AddSingleton<PasswordRecoveryViewModel>(services => new PasswordRecoveryViewModel(
                    services.GetRequiredService<IUnitOfWork>(),
                    services.GetRequiredService<ChangeViewCommand>(),
                    services.GetRequiredService<IEmailService>(),
                    services.GetRequiredService<IPasswordHasher>())
                  );

            services.AddSingleton<CreateViewModel<PasswordRecoveryViewModel>>(services =>
            {
                return () => services.GetRequiredService<PasswordRecoveryViewModel>();
            });

            services.AddScoped<AdminPanelViewModel>(services => new AdminPanelViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<IUnitOfWork>())
                  );

            services.AddSingleton<CreateViewModel<AdminPanelViewModel>>(services =>
            {
                return () => services.GetRequiredService<AdminPanelViewModel>();
            });

            services.AddScoped<SettingsViewModel>(services => new SettingsViewModel(
                   services.GetRequiredService<IAuthenticator>(),                   
                   services.GetRequiredService<IUnitOfWork>(),
                   services.GetRequiredService<IEmailService>())
                 );

            services.AddSingleton<CreateViewModel<SettingsViewModel>>(services =>
            {
                return () => services.GetRequiredService<SettingsViewModel>();
            });

            services.AddSingleton<ProfileViewModelBuilder>(services => ProfileViewModelBuilder.Init(
                 services.GetRequiredService<IUnitOfWork>(),
                 services.GetRequiredService<ChangeViewCommand>()
                 )
               );

            services.AddTransient<CreateViewModelWithParam<ProfileViewModel, User>>(services =>
            {
                return param => services.GetRequiredService<ProfileViewModelBuilder>().SetUser(param).Build();
            });

            services.AddSingleton<ActorViewModelBuilder>(services => ActorViewModelBuilder.Init(
                  services.GetRequiredService<IUnitOfWork>(),
                  services.GetRequiredService<ChangeViewCommand>()
                  )
                );

            services.AddTransient<CreateViewModelWithParam<ActorViewModel, Actor>>(services =>
            {
                return param => services.GetRequiredService<ActorViewModelBuilder>().SetActor(param).Build();
            });

            services.AddSingleton<RateFilmPanelViewModelBuilder>(services => RateFilmPanelViewModelBuilder.Init(
                  services.GetRequiredService<IAuthenticator>(),
                  services.GetRequiredService<IUnitOfWork>(),
                  services.GetRequiredService<ILeaveReviewService>()
                  )
                );

            services.AddSingleton<UserReviewsPanelViewModelBuilder>(services => UserReviewsPanelViewModelBuilder.Init(
                 services.GetRequiredService<IUnitOfWork>(),
                 services.GetRequiredService<IAuthenticator>(),
                 services.GetRequiredService<ILeaveReviewService>(),
                 services.GetRequiredService<ChangeViewCommand>()
                 )
               );

            services.AddSingleton<FilmCastListViewModelBuilder>(services => FilmCastListViewModelBuilder.Init(
                 services.GetRequiredService<IUnitOfWork>(),
                 services.GetRequiredService<ChangeViewCommand>()
                 )
               );


            services.AddTransient<CreateViewModelWithParam<FilmViewModel, Film>>(services =>
            {
                return param => FilmViewModelBuilder.Init(services.GetRequiredService<RateFilmPanelViewModelBuilder>(),
                                                          services.GetRequiredService<UserReviewsPanelViewModelBuilder>(),
                                                          services.GetRequiredService<FilmCastListViewModelBuilder>(),
                                                          services.GetRequiredService<IUnitOfWork>(),
                                                          services.GetRequiredService<IAuthenticator>()).SetFilm(param).Build();  
            });

            services.AddSingleton<AppHeaderViewModel>(services => new AppHeaderViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<IUnitOfWork>(),
                    services.GetRequiredService<ChangeViewCommand>(),
                    services.GetRequiredService<Renavigator<LoginViewModel>>())
                  );

            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
             
        }
    }
}
