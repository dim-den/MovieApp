using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class ChangeViewCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        public ChangeViewCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public ChangeViewCommand(INavigator navigator, IAuthenticator authenticator = null)
        {
            _navigator = navigator;
            _authenticator = authenticator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var unitOfWork = new UnitOfWork();

            if (parameter is Film film)
            {
                var filmCastStore = new Store<FilmCast>(unitOfWork.FilmCastRepository);
                var filmReviewStore = new Store<FilmReview>(unitOfWork.FilmReviewRepository);

                await filmCastStore.LoadWithInclude(f => f.FilmID == film.ID, f => f.Actor);
                await filmReviewStore.LoadWithInclude(r => r.FilmID == film.ID && !string.IsNullOrEmpty(r.ReviewText), r => r.User);
                var userFilmReview = await unitOfWork.FilmReviewRepository.GetUserFilmReview(_authenticator.CurrentUser.ID, film.ID);

                _navigator.CurrentViewModel = new FilmViewModel(_navigator,
                                                                _authenticator,
                                                                filmCastStore,
                                                                filmReviewStore,
                                                                film,
                                                                userFilmReview);
            }
            else if (parameter is Actor actor)
            {
                var actorFilmCastStore = new Store<FilmCast>(unitOfWork.FilmCastRepository);
                await actorFilmCastStore.LoadWithInclude(c => c.ActorID == actor.ID, c => c.Film);

                _navigator.CurrentViewModel = new ActorViewModel(_navigator, _authenticator, actor, actorFilmCastStore);
            }
            else if (parameter is User user)
            {
                var userFilmReviewsStore = new Store<FilmReview>(unitOfWork.FilmReviewRepository);

                await userFilmReviewsStore.LoadWithInclude(f => f.UserID == user.ID, f => f.Film);

                _navigator.CurrentViewModel = new ProfileViewModel(_navigator, _authenticator, user, userFilmReviewsStore);
            }
            else if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.Login:
                        _navigator.CurrentViewModel = new LoginViewModel(_navigator, _authenticator);
                        break;
                    case ViewType.Register:
                        _navigator.CurrentViewModel = new RegisterViewModel(_navigator, _authenticator);
                        break;
                    case ViewType.PasswordRecovery:
                        _navigator.CurrentViewModel = new PasswordRecoveryViewModel(_navigator, _authenticator, unitOfWork);
                        break;
                    case ViewType.Home:
                        var filmStore = new Store<Film>(unitOfWork.FilmRepository);
                        var actorStore = new Store<Actor>(unitOfWork.ActorRepository);

                        await filmStore.Load();
                        await actorStore.Load();

                        _navigator.CurrentViewModel = new HomeViewModel(_navigator, _authenticator, filmStore, actorStore);
                        break;
                    case ViewType.AdminPanel:
                        _navigator.CurrentViewModel = new AdminPanelViewModel(_authenticator, unitOfWork);
                        break;
                    case ViewType.Settings:
                        _navigator.CurrentViewModel = new SettingsViewModel(_navigator, _authenticator, unitOfWork);
                        break;
                }
            }
        }
    }
}
