using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using MovieApp.Domain.Models;
using MovieApp.WPF.Commands;

namespace MovieApp.WPF.ViewModels
{
    public class MovieCarouselViewModel : ViewModelBase
    {
        public ICommand ChangeMovieCarouselCommand { get; }
        public ICommand ChangeViewCommand { get; }

        private ObservableCollection<Film> _films;

        public ObservableCollection<Film> Films
        {
            get => _films;
            set
            {
                _films = value;
                OnPropertyChanged(nameof(Films));
            }
        }

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
            }
        }

        public Film CurrentFilm => _films?[CurrentIndex];
        public byte[] PosterImageData => CurrentFilm?.PosterImageData;

        private readonly DispatcherTimer _timer;
        public DispatcherTimer Timer => _timer;

        public MovieCarouselViewModel(ICommand changeViewCommand)
        {
            ChangeViewCommand = changeViewCommand;

            _timer = new DispatcherTimer();
            CurrentIndex = 0;

            Timer.Tick += TimerTick;
            Timer.Interval = TimeSpan.FromSeconds(5);
            Timer.Start();

            ChangeMovieCarouselCommand = new ChangeMovieCarouselCommand(this);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ChangeMovieCarouselCommand.Execute(ChangeDirection.Right);
        }
    }
}