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
    public class ProfileViewModelBuilder : IViewModelBuilder<ProfileViewModel>
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly User _user;

        private IStore<FilmReview> _userFilmReviewsStore;

        public async Task<ProfileViewModelBuilder> LoadUserFilmReviews()
        {
            await _userFilmReviewsStore.LoadWithInclude(f => f.UserID == _user.ID, f => f.Film);

            return this;
        }

        private ProfileViewModelBuilder(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork, User user)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _unitOfWork = unitOfWork;
            _user = user;

            _userFilmReviewsStore = new Store<FilmReview>(_unitOfWork.FilmReviewRepository);
        }

        public static ProfileViewModelBuilder Init(INavigator navigator, IAuthenticator authenticator, IUnitOfWork unitOfWork, User user)
        {
            return new ProfileViewModelBuilder(navigator, authenticator, unitOfWork, user);
        }

        public ProfileViewModel Build()
        {
            return new ProfileViewModel(_navigator,
                                        _authenticator,
                                        _user,
                                        _userFilmReviewsStore);
        }
    }
}
