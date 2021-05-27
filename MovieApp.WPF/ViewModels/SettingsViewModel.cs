using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

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
      
        public SettingsViewModel(INavigator navigator, IAuthenticator authentificator, IUnitOfWork unitOfWork)
        {
            _authentificator = authentificator;

            ChangeImageCommand = new ChangeImageCommand(_authentificator, unitOfWork);

            PasswordChangePanelViewModel = new PasswordChangePanelViewModel(_authentificator);
            EmailConfirmPanelViewModel = new EmailConfirmPanelViewModel(_authentificator, unitOfWork);
            AppLanguagePanelViewModel = new AppLanguagePanelViewModel();

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
        }
    }
}
