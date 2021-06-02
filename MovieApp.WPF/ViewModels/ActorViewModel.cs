using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

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

        public bool HasFilmCast => ActorFilmCast?.Count() > 0;

        public string FilmingTime => HasFilmCast ? $"{ActorFilmCast.Min(f => f.Film.ReleaseDate).Year} - {ActorFilmCast.Max(f => f.Film.ReleaseDate).Year}" : "no info";

        public ActorViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _unitOfWork = unitOfWork;

            ChangeViewCommand = changeViewCommand;
        }
    }
}