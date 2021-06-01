﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
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
        private readonly IUnitOfWork _unitOfWork;
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
        public MovieCarouselViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _unitOfWork = unitOfWork;

            ChangeViewCommand = changeViewCommand;

            _timer = new DispatcherTimer();
            CurrentIndex = 0;

            Timer.Tick += TimerTick;
            Timer.Interval = TimeSpan.FromSeconds(5);
            Timer.Start();

            ChangeMovieCarouselCommand = new ChangeMovieCarouselCommand(this);
        }

        public static MovieCarouselViewModel LoadMovieCarouselViewModel(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            MovieCarouselViewModel majorIndexViewModel = new MovieCarouselViewModel(unitOfWork, changeViewCommand);
            majorIndexViewModel.LoadFilms();

            return majorIndexViewModel;
        }

        private void LoadFilms()
        {
            _unitOfWork.FilmRepository.GetRandomReleasedFilms(5).ContinueWith(task =>
            {
                if(task.Exception == null)
                {
                    Films = new ObservableCollection<Film>(task.Result);
                }
            });
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ChangeMovieCarouselCommand.Execute(ChangeDirection.Right);
        }
    }
}
