using MovieApp.Domain.Models;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModelWithParam<ActorViewModel, Actor> _createActorViewModel;
        private readonly CreateViewModel<AdminPanelViewModel> _createAdminViewModel;
        private readonly CreateViewModelWithParam<FilmViewModel, Film> _createFilmViewModel;
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<PasswordRecoveryViewModel> _createPasswordRecoveryViewModel;
        private readonly CreateViewModelWithParam<ProfileViewModel, User> _createProfileViewModel;
        private readonly CreateViewModel<RegisterViewModel> _createRegisterViewModel;
        private readonly CreateViewModel<SettingsViewModel> _createSettingsViewModel;

        public ViewModelFactory(CreateViewModelWithParam<ActorViewModel, Actor> createActorViewModel,
                                CreateViewModel<AdminPanelViewModel> createAdminViewModel,
                                CreateViewModelWithParam<FilmViewModel, Film> createFilmViewModel,
                                CreateViewModel<HomeViewModel> createHomeViewModel,
                                CreateViewModel<LoginViewModel> createLoginViewModel,
                                CreateViewModel<PasswordRecoveryViewModel> createPasswordRecoveryViewModel,
                                CreateViewModelWithParam<ProfileViewModel, User> createProfileViewModel,
                                CreateViewModel<RegisterViewModel> createRegisterViewModel,
                                CreateViewModel<SettingsViewModel> createSettingsViewModel)
        {
            _createActorViewModel = createActorViewModel;
            _createAdminViewModel = createAdminViewModel;
            _createFilmViewModel = createFilmViewModel;
            _createHomeViewModel = createHomeViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createPasswordRecoveryViewModel = createPasswordRecoveryViewModel;
            _createProfileViewModel = createProfileViewModel;
            _createRegisterViewModel = createRegisterViewModel;
            _createSettingsViewModel = createSettingsViewModel;
        }

        public ViewModelBase CreateViewModel(object parameter)
        {
            if (parameter is Film film)
            {
                return _createFilmViewModel(film);
            }
            else if (parameter is Actor actor)
            {
                return _createActorViewModel(actor);
            }
            else if (parameter is User user)
            {
                return _createProfileViewModel(user);
            }
            else if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.Login:
                        return _createLoginViewModel();

                    case ViewType.Register:
                        return _createRegisterViewModel();

                    case ViewType.PasswordRecovery:
                        return _createPasswordRecoveryViewModel();

                    case ViewType.Home:
                        return _createHomeViewModel();

                    case ViewType.AdminPanel:
                        return _createAdminViewModel();

                    case ViewType.Settings:
                        return _createSettingsViewModel();
                }
            }

            return null;
        }
    }
}