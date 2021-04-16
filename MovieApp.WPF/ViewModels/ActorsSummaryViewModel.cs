using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorsSummaryViewModel : ViewModelBase
    {
        public ActorsListingViewModel ActorsListingViewModel { get; }
        public ActorsSummaryViewModel(INavigator navigator, IStore<Actor> actorStore)
        {
            ActorsListingViewModel = new ActorsListingViewModel(actorStore);
        }
    }
}
