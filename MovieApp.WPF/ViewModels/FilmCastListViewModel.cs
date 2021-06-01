using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels
{
    public class FilmCastListViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }

        private ObservableCollection<FilmCast> _cast;
        public ObservableCollection<FilmCast> Cast
        {
            get => _cast;
            set
            {
                _cast = value;
                OnPropertyChanged(nameof(Cast));
            }
        }

        private Film _film;
        public Film Film
        {
            get => _film;
            set
            {
                _film = value;
                OnPropertyChanged(nameof(Film));
            }
        }

        public FilmCastListViewModel(ICommand changeViewCommand)
        {
            ChangeViewCommand = changeViewCommand;
        }
        
    }
}
