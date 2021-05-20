using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class MovieCarouselViewModel : ViewModelBase
    {
        public ICommand ChangeMovieCarouselCommand { get; }
        public ICommand ChangeViewCommand { get; }

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

        private readonly DispatcherTimer _timer;
        public DispatcherTimer Timer => _timer;
        public MovieCarouselViewModel(INavigator navigator, IAuthenticator authentificator, IStore<Film> filmStore)
        {
            var rnd = new Random();
            _films = new ObservableCollection<Film>(filmStore.Entities.Where(f => f.ReleaseDate <= DateTime.Now).OrderBy(x => rnd.Next()).Take(5));
            _timer = new DispatcherTimer();
            CurrentIndex = 0;

            Timer.Tick += TimerTick;
            Timer.Interval = TimeSpan.FromSeconds(5);
            Timer.Start();

            ChangeMovieCarouselCommand = new ChangeMovieCarouselCommand(this);
            ChangeViewCommand = new ChangeViewCommand(navigator, authentificator);
        }
        private void TimerTick(object sender, EventArgs e)
        {
            ChangeMovieCarouselCommand.Execute(ChangeDirection.Right);
        }
    }
}
