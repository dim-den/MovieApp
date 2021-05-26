using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels.Builders;

namespace MovieApp.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        public ViewModelFactory(INavigator navigator, IAuthenticator authenticator = null)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _unitOfWork = new UnitOfWork();
        }

        public async Task<ViewModelBase> CreateViewModel(object viewType)
        {
            if (viewType is Film film)
            {
                return (await (await (await FilmViewModelBuilder.Init(_navigator, _authenticator, _unitOfWork, film)
                                                                .LoadFilmCast())
                                                                .LoadFilmReviews())
                                                                .LoadCurrentUserFilmReview())
                                                                .Build();
            }
            else if (viewType is Actor actor)
            {
                return (await ActorViewModelBuilder.Init(_navigator, _authenticator, _unitOfWork, actor)
                                                   .LoadFilmCast())
                                                   .Build();
            }
            else if (viewType is User user)
            {
                return (await ProfileViewModelBuilder.Init(_navigator, _authenticator, _unitOfWork, user)
                                                     .LoadUserFilmReviews())
                                                     .Build();
            }
            else if (viewType is ViewType type)
            {
                switch (type)
                {
                    case ViewType.Login:
                        return new LoginViewModel(_navigator, _authenticator);

                    case ViewType.Register:
                        return new RegisterViewModel(_navigator, _authenticator);

                    case ViewType.PasswordRecovery:
                        return new PasswordRecoveryViewModel(_navigator, _authenticator, _unitOfWork);

                    case ViewType.Home:
                        return (await (await (await HomeViewModelBuilder.Init(_navigator, _authenticator, _unitOfWork)
                                                                        .LoadRandomFilms(5))
                                                                        .LoadRandomActors(9))
                                                                        .LoadUpcomingFilms(4))
                                                                        .Build();

                    case ViewType.AdminPanel:
                        return new AdminPanelViewModel(_authenticator, _unitOfWork);

                    case ViewType.Settings:
                        return new SettingsViewModel(_navigator, _authenticator, _unitOfWork);

                }
            }

            return null;
        }
    }
}