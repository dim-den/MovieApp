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
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorsSummaryViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ICommand ChangeViewCommand { get; }

        private ObservableCollection<Actor> _actors;
        public ObservableCollection<Actor> Actors
        {
            get => _actors;
            set
            {
                _actors = value;
                OnPropertyChanged(nameof(Actors));
            }
        }
        public ActorsSummaryViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _unitOfWork = unitOfWork;
            ChangeViewCommand = changeViewCommand;
        }

        public static ActorsSummaryViewModel LoadActorsSummaryViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            ActorsSummaryViewModel actorsSummaryViewModel = new ActorsSummaryViewModel(unitOfWork, changeViewCommand);
            actorsSummaryViewModel.LoadActors();

            return actorsSummaryViewModel;       
        }

        private void LoadActors()
        {
            _unitOfWork.ActorRepository.GetRandomEntities(9).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Actors = new ObservableCollection<Actor>(task.Result);
                }
            });
        }
    }
}
