using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class ActorViewModelBuilder : IViewModelBuilder<ActorViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private ActorViewModel _actorViewModel;

        private ActorViewModelBuilder(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _actorViewModel = new ActorViewModel(unitOfWork, changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public ActorViewModelBuilder SetActor(Actor actor)
        {
            _actorViewModel.Actor = actor;
            LoadActorFilmCast();

            return this;
        }

        public static ActorViewModelBuilder Init(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            return new ActorViewModelBuilder(unitOfWork, changeViewCommand);
        }

        public ActorViewModel Build()
        {
            return _actorViewModel;
        }

        private void LoadActorFilmCast()
        {
            _unitOfWork.FilmCastRepository.GetWithInclude(c => c.ActorID == _actorViewModel.Actor.ID, c => c.Film).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _actorViewModel.ActorFilmCast = new ObservableCollection<FilmCast>(task.Result.OrderByDescending(f => f.Film.ReleaseDate));
                }
            });
        }
    }
}