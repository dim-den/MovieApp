using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand ChangeImageCommand { get; set; }

        public PasswordChangePanelViewModel PasswordChangePanelViewModel { get; }
        public EmailConfirmPanelViewModel EmailConfirmPanelViewModel { get; }
        public AppLanguagePanelViewModel AppLanguagePanelViewModel { get; }

        private readonly IAuthenticator _authentificator;

        public User CurrentUser => _authentificator.CurrentUser;

        public SettingsViewModel(IAuthenticator authentificator, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _authentificator = authentificator;

            ChangeImageCommand = new ChangeImageCommand(_authentificator, unitOfWork);

            PasswordChangePanelViewModel = new PasswordChangePanelViewModel(_authentificator);
            EmailConfirmPanelViewModel = new EmailConfirmPanelViewModel(_authentificator, unitOfWork, emailService);
            AppLanguagePanelViewModel = new AppLanguagePanelViewModel();

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
        }
    }
}