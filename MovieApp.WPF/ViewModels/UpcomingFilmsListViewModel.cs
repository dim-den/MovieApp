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
    public class UpcomingFilmsListViewModel : ViewModelBase
    {
        public ObservableCollection<Film> UpcomingFilms { get; }
        public UpcomingFilmsListViewModel(IStore<Film> filmStore)
        {
            UpcomingFilms = new ObservableCollection<Film>(filmStore.Entities.Where(f => f.ReleaseDate > DateTime.Now).Take(4).OrderBy(d => d.ReleaseDate));
        }
    }
}
