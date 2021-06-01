using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;
using MovieApp.WPF.ViewModels.Factories;

namespace MovieApp.WPF.ViewModels
{
    public class AppHeaderViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authentificator;
        private readonly IUnitOfWork _unitOfWork;

        private SearchBarViewModel _searchBarViewModel;
        public SearchBarViewModel SearchBarViewModel
        {
            get => _searchBarViewModel;
            set
            {
                _searchBarViewModel = value;
                OnPropertyChanged(nameof(SearchBarViewModel));
            }
        }

        public ICommand ChangeViewCommand { get; }
        public ICommand SignOutCommand { get; }


        public User CurrentUser => _authentificator.CurrentUser;

        public bool IsAdmin => CurrentUser != null && CurrentUser.ClientType >= ClientType.Admin;

        public byte[] ImageData => CurrentUser?.ImageData;

        public AppHeaderViewModel(IAuthenticator authentificator, IUnitOfWork unitOfWork, ICommand changeViewCommand, IRenavigator loginRenavigate)
        {
            _authentificator = authentificator;
            _unitOfWork = unitOfWork;

            SignOutCommand = new SignOutCommand(authentificator, loginRenavigate);
            ChangeViewCommand = changeViewCommand;
            LoadSearchBar();

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void LoadSearchBar()
        {
            ICollection<Film> films = _unitOfWork.FilmRepository.GetAllSync();
            ICollection<Actor> actors = null;

            _unitOfWork.ActorRepository.GetAll().ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    actors = task.Result;
                    SearchBarViewModel = new SearchBarViewModel(films, actors, ChangeViewCommand);
                }
            });
            
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(ImageData));
            OnPropertyChanged(nameof(IsAdmin));
        }
    }
}