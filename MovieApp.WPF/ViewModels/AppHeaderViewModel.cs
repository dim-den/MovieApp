using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class AppHeaderViewModel : ViewModelBase
    {
        private User _currentUser;
        public ICommand ChangeViewCommand { get; set; }
        public ICommand SignOutCommand { get; set; }
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(ImageData));
            }
        }
        public string Name => _currentUser?.Name ?? "";
        public byte[] ImageData => _currentUser?.ImageData ?? null; 
        public AppHeaderViewModel(INavigator navigator, IAuthenticator authentificator)
        {
            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);
            SignOutCommand = new SignOutCommand(navigator, authentificator);

            _currentUser = new User();
        }
    }
}