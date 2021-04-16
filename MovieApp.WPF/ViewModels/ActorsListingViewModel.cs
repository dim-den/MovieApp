using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorsListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Actor> _actors;
        public ObservableCollection<Actor> Actors => _actors;
        public ActorsListingViewModel(IStore<Actor> actorStore)
        {
            _actors = new ObservableCollection<Actor>(actorStore.Entities.Take(6));
        }
    }
}
