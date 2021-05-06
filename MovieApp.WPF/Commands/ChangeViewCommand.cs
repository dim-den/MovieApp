using System;
using System.Collections.Generic;
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

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ViewType viewType = (ViewType)parameter;
            using (var unitOfWork = new UnitOfWork())
            {
                switch (viewType)
                {
                    case ViewType.Login:
                        _navigator.CurrentViewModel = new LoginViewModel(_navigator, _authenticator);
                        break;
                    case ViewType.Register:
                        _navigator.CurrentViewModel = new RegisterViewModel(_navigator, _authenticator);
                        break;
                    case ViewType.Home:
                        var filmStore = new Store<Film>(unitOfWork.FilmRepository);
                        var actorStore = new Store<Actor>(unitOfWork.ActorRepository);

                        await filmStore.Load();
                        await actorStore.Load();

                        _navigator.CurrentViewModel = new HomeViewModel(_navigator, filmStore, actorStore);
                        break;
                    case ViewType.Profile:
                        var userFilmReviewsStore = new Store<FilmReview>(unitOfWork.FilmReviewRepository);

                        await userFilmReviewsStore.LoadWithInclude(f => f.UserID == _authenticator.CurrentUser.ID, f => f.Film);

                        _navigator.CurrentViewModel = new ProfileViewModel(_navigator, _authenticator, userFilmReviewsStore);
                        break;
                    case ViewType.Film:
                        var filmCastStore = new Store<FilmCast>(unitOfWork.FilmCastRepository);
                        var filmReviewStore = new Store<FilmReview>(unitOfWork.FilmReviewRepository);
                        int filmID = 3;     // TODO remove

                        await filmCastStore.LoadWithInclude(f => f.FilmID == filmID, f => f.Actor);
                        await filmReviewStore.LoadWithInclude(r => r.FilmID == filmID && !string.IsNullOrEmpty(r.ReviewText), r => r.User);
                        var userFilmReview = await unitOfWork.FilmReviewRepository.GetUserFilmReview(_authenticator.CurrentUser.ID, filmID);

                        _navigator.CurrentViewModel = new FilmViewModel(_navigator, 
                                                                        _authenticator, 
                                                                        filmCastStore, 
                                                                        filmReviewStore, 
                                                                        await unitOfWork.FilmRepository.Get(filmID),
                                                                        userFilmReview);
                        break;
                    case ViewType.Actor:
                        int actorID = 1;
                        var actorFilmCastStore = new Store<FilmCast>(unitOfWork.FilmCastRepository);
                        await actorFilmCastStore.LoadWithInclude(c => c.ActorID == actorID, c => c.Film);

                        _navigator.CurrentViewModel = new ActorViewModel(await unitOfWork.ActorRepository.Get(actorID), actorFilmCastStore);
                        break;
                    case ViewType.AdminPanel:
                        _navigator.CurrentViewModel = new AdminPanelViewModel(_authenticator, new UnitOfWork());
                        break;
                }
            }
        }
    }
}
