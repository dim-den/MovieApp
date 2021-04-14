using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.FilmServices;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class MovieCarouselViewModel : ViewModelBase
    {
        public ICommand ChangeMovieCarouselCommand { get; set; }

        private readonly ObservableCollection<Film> _films;
        public ObservableCollection<Film> Films => _films;

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                OnPropertyChanged(nameof(CurrentFilm));
                OnPropertyChanged(nameof(PosterImageData));
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Year));
            }
        }
        public Film CurrentFilm => _films[CurrentIndex];
        public byte[] PosterImageData => CurrentFilm.PosterImageData;
        public string Title => CurrentFilm.Title;
        public int Year => CurrentFilm.ReleaseDate.Year;

        private DispatcherTimer _timer;
        public DispatcherTimer Timer => _timer;
        public MovieCarouselViewModel(INavigator navigator, IFilmStore filmStore)
        {
            _films = new ObservableCollection<Film>(filmStore.Films.Take(Math.Min(filmStore.Films.Count, 5)));
            _timer = new DispatcherTimer();
            CurrentIndex = 0;

            Timer.Tick += TimerTick;
            Timer.Interval = TimeSpan.FromSeconds(10);
            Timer.Start();

            ChangeMovieCarouselCommand = new ChangeMovieCarouselCommand(this);
        }
        private void TimerTick(object sender, EventArgs e)
        {
            ChangeMovieCarouselCommand.Execute(ChangeDirection.Right);
        }
    }
}
