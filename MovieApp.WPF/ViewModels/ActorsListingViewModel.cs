using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorsListingViewModel : ViewModelBase
    {
        public ICommand GoToActorCommand { get; }

        private readonly ObservableCollection<Actor> _actors;

        public ObservableCollection<Actor> Actors => _actors;

        public ActorsListingViewModel(INavigator navigator, IAuthenticator authentificator, IUnitOfWork unitOfWork, IStore<Actor> actorStore)
        {
            _actors = new ObservableCollection<Actor>(actorStore.Entities.Take(9));

            GoToActorCommand = new GoToActorCommand(navigator, authentificator, unitOfWork);
        }
    }
}
