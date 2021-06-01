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
    public class ActorViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ICommand ChangeViewCommand { get; }

        private Actor _actor;
        public Actor Actor
        {
            get => _actor;
            set
            {
                _actor = value;
                OnPropertyChanged(nameof(Actor));
            }
        }

        private ObservableCollection<FilmCast> _actorFilmCast;
        public ObservableCollection<FilmCast> ActorFilmCast
        {
            get => _actorFilmCast;
            set
            {
                _actorFilmCast = value;
                OnPropertyChanged(nameof(ActorFilmCast));
                OnPropertyChanged(nameof(HasFilmCast));
                OnPropertyChanged(nameof(FilmingTime));
            }
        }

        public bool HasFilmCast => ActorFilmCast != null && ActorFilmCast.Count() > 0;

        public string FilmingTime => HasFilmCast ? $"{ActorFilmCast.Min(f => f.Film.ReleaseDate).Year} - {ActorFilmCast.Max(f => f.Film.ReleaseDate).Year}" : "no info";

        public ActorViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _unitOfWork = unitOfWork;

            ChangeViewCommand = changeViewCommand;
        }
      
    }
}
