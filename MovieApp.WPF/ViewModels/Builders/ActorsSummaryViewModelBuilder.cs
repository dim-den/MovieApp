using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class ActorsSummaryViewModelBuilder : IViewModelBuilder<ActorsSummaryViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private ActorsSummaryViewModel _actorsSummaryViewModel;

        private ActorsSummaryViewModelBuilder(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _actorsSummaryViewModel = new ActorsSummaryViewModel(changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public ActorsSummaryViewModelBuilder SetActorsCount(int count)
        {
            LoadActors(count);

            return this;
        }

        public static ActorsSummaryViewModelBuilder Init(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            return new ActorsSummaryViewModelBuilder(unitOfWork, changeViewCommand);
        }

        public ActorsSummaryViewModel Build()
        {
            return _actorsSummaryViewModel;
        }

        private void LoadActors(int count)
        {
            _unitOfWork.ActorRepository.GetRandomEntities(count).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _actorsSummaryViewModel.Actors = new ObservableCollection<Actor>(task.Result);
                }
            });
        }
    }
}
