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
                var filmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
                var filmReviewStore = new Store<FilmReview>(_unitOfWork.FilmReviewRepository);

                await filmCastStore.LoadWithInclude(f => f.FilmID == film.ID, f => f.Actor);
                await filmReviewStore.LoadWithInclude(r => r.FilmID == film.ID && !string.IsNullOrEmpty(r.ReviewText), r => r.User);
                var userFilmReview = await _unitOfWork.FilmReviewRepository.GetUserFilmReview(_authenticator.CurrentUser.ID, film.ID);

                return new FilmViewModel(_navigator,
                                         _authenticator,
                                         filmCastStore,
                                         filmReviewStore,
                                         film,
                                         userFilmReview);
            }
            else if (viewType is Actor actor)
            {
                var actorFilmCastStore = new Store<FilmCast>(_unitOfWork.FilmCastRepository);
                await actorFilmCastStore.LoadWithInclude(c => c.ActorID == actor.ID, c => c.Film);

                return new ActorViewModel(_navigator, _authenticator, actor, actorFilmCastStore);
            }
            else if (viewType is User user)
            {
                var userFilmReviewsStore = new Store<FilmReview>(_unitOfWork.FilmReviewRepository);

                await userFilmReviewsStore.LoadWithInclude(f => f.UserID == user.ID, f => f.Film);

                return new ProfileViewModel(_navigator, _authenticator, user, userFilmReviewsStore);
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
                        var filmStore = new Store<Film>(_unitOfWork.FilmRepository);
                        var actorStore = new Store<Actor>(_unitOfWork.ActorRepository);

                        await filmStore.Load();
                        await actorStore.Load();

                        return new HomeViewModel(_navigator, _authenticator, filmStore, actorStore);

                    case ViewType.AdminPanel:
                        return new AdminPanelViewModel(_authenticator, _unitOfWork);

                    case ViewType.Settings:
                        return new SettingsViewModel(_navigator, _authenticator, _unitOfWork);

                }
            }

            throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
        }
    }
}