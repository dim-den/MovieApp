using System;
using System.Windows.Input;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public enum ChangeDirection
    {
        Left,
        Right
    }

    public class ChangeMovieCarouselCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MovieCarouselViewModel _movieCarouselViewModel;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ChangeDirection direction = (ChangeDirection)parameter;
            int filmsCount = _movieCarouselViewModel.Films.Count;

            if (direction == ChangeDirection.Left)
            {
                if (_movieCarouselViewModel.CurrentIndex == 0)
                {
                    _movieCarouselViewModel.CurrentIndex = filmsCount - 1;
                }
                else
                {
                    _movieCarouselViewModel.CurrentIndex--;
                }
            }
            else
            {
                if (_movieCarouselViewModel.CurrentIndex == filmsCount - 1)
                {
                    _movieCarouselViewModel.CurrentIndex = 0;
                }
                else
                {
                    _movieCarouselViewModel.CurrentIndex++;
                }
            }

            _movieCarouselViewModel.Timer.Stop();
            _movieCarouselViewModel.Timer.Start();
        }

        public ChangeMovieCarouselCommand(MovieCarouselViewModel movieCarouselViewModel)
        {
            _movieCarouselViewModel = movieCarouselViewModel;
        }
    }
}