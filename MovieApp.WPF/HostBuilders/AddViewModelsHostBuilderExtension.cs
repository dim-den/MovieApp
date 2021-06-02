using System;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.ViewModels;
using MovieApp.WPF.ViewModels.Builders;
using MovieApp.WPF.ViewModels.Factories;

namespace MovieApp.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                services.AddSingleton<ChangeViewCommand>();

                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModelBuilder>()
                                                                                        .SetCarouselFilmsCount(5)
                                                                                        .SetActorsCount(9)
                                                                                        .SetUpcomingFilmsCount(4)
                                                                                        .Build());
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
                services.AddSingleton<CreateViewModel<PasswordRecoveryViewModel>>(services => () => CreatePasswordRecoveryViewModel(services));
                services.AddSingleton<CreateViewModel<AdminPanelViewModel>>(services => () => CreateAdminPanelViewModel(services));
                services.AddSingleton<CreateViewModel<SettingsViewModel>>(services => () => CreateSettingsViewModel(services));
                services.AddTransient<CreateViewModelWithParam<ProfileViewModel, User>>(services =>
                    (param) => services.GetRequiredService<ProfileViewModelBuilder>().SetUser(param).Build());

                services.AddTransient<CreateViewModelWithParam<ActorViewModel, Actor>>(services =>
                    (param) => services.GetRequiredService<ActorViewModelBuilder>().SetActor(param).Build());

                services.AddTransient<CreateViewModelWithParam<FilmViewModel, Film>>(services =>
                    (param) => services.GetRequiredService<FilmViewModelBuilder>().SetFilm(param).Build());

                services.AddSingleton<Renavigator<HomeViewModel>>();
                services.AddSingleton<Renavigator<LoginViewModel>>();

                services.AddSingleton<AppHeaderViewModel>(CreateAppHeaderViewModel);
                services.AddSingleton<MainViewModel>();
            });

            return host;
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(services.GetRequiredService<IAuthenticator>(),
                                      services.GetRequiredService<ChangeViewCommand>(),
                                      services.GetRequiredService<Renavigator<HomeViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(services.GetRequiredService<IAuthenticator>(),
                                         services.GetRequiredService<ChangeViewCommand>(),
                                         services.GetRequiredService<Renavigator<HomeViewModel>>());
        }

        private static PasswordRecoveryViewModel CreatePasswordRecoveryViewModel(IServiceProvider services)
        {
            return new PasswordRecoveryViewModel(services.GetRequiredService<IUnitOfWork>(),
                                                 services.GetRequiredService<ChangeViewCommand>(),
                                                 services.GetRequiredService<EmailService>(),
                                                 services.GetRequiredService<IPasswordHasher>());
        }

        private static AdminPanelViewModel CreateAdminPanelViewModel(IServiceProvider services)
        {
            return new AdminPanelViewModel(services.GetRequiredService<IAuthenticator>(),
                                           services.GetRequiredService<IUnitOfWork>());
        }

        private static SettingsViewModel CreateSettingsViewModel(IServiceProvider services)
        {
            return new SettingsViewModel(services.GetRequiredService<IAuthenticator>(),
                                         services.GetRequiredService<IUnitOfWork>(),
                                         services.GetRequiredService<EmailService>());
        }

        private static AppHeaderViewModel CreateAppHeaderViewModel(IServiceProvider services)
        {
            return new AppHeaderViewModel(services.GetRequiredService<IAuthenticator>(),
                                          services.GetRequiredService<IUnitOfWork>(),
                                          services.GetRequiredService<ChangeViewCommand>(),
                                          services.GetRequiredService<Renavigator<LoginViewModel>>());
        }
    }
}