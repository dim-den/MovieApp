using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class UpcomingFilmsListViewModel : ViewModelBase
    {
        public ICommand GoToFilmCommand { get; }

        public ObservableCollection<Film> UpcomingFilms { get; }

        public UpcomingFilmsListViewModel(INavigator navigator, IAuthenticator authentificator, IStore<Film> filmStore)
        {
            UpcomingFilms = new ObservableCollection<Film>(filmStore.Entities.Where(f => f.ReleaseDate > DateTime.Now).Take(4).OrderBy(d => d.ReleaseDate));

            GoToFilmCommand = new GoToFilmCommand(navigator, authentificator, new UnitOfWork());
        }
    }
}
