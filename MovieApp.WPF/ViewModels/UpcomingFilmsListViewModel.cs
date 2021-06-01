using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class UpcomingFilmsListViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ICommand  ChangeViewCommand { get; }

        private ObservableCollection<Film> _upcomingFilms;
        public ObservableCollection<Film> UpcomingFilms
        {
            get => _upcomingFilms;
            set
            {
                _upcomingFilms = value;
                OnPropertyChanged(nameof(UpcomingFilms));
            }
        }
        public UpcomingFilmsListViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _unitOfWork = unitOfWork;
            ChangeViewCommand = changeViewCommand;
        }

        public static UpcomingFilmsListViewModel LoadUpcomingFilmsListViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            UpcomingFilmsListViewModel upcomingFilmsListViewModel = new UpcomingFilmsListViewModel(unitOfWork, changeViewCommand);
            upcomingFilmsListViewModel.LoadFilms();

            return upcomingFilmsListViewModel;
        }

        private void LoadFilms()
        {
            _unitOfWork.FilmRepository.GetUpcomingFilms().ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    UpcomingFilms = new ObservableCollection<Film>(task.Result.Take(4));
                }
            });
        }
    }
}
