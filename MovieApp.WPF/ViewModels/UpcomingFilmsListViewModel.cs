using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.ViewModels
{
    public class UpcomingFilmsListViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }

        private ObservableCollection<Film> _upcomingFilms;

        public ObservableCollection<Film> UpcomingFilms
        {
            get => _upcomingFilms;
            set
            {
                _upcomingFilms = value;
                OnPropertyChanged(nameof(UpcomingFilms));
            }
        }

        public UpcomingFilmsListViewModel(ICommand changeViewCommand)
        {
            ChangeViewCommand = changeViewCommand;
        }
    }
}