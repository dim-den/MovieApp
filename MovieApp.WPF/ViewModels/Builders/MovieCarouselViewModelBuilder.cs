using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class MovieCarouselViewModelBuilder : IViewModelBuilder<MovieCarouselViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private MovieCarouselViewModel _movieCarouselViewModel;

        private MovieCarouselViewModelBuilder(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _movieCarouselViewModel = new MovieCarouselViewModel(changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public MovieCarouselViewModelBuilder SetFilmCount(int count)
        {
            LoadFilms(count);

            return this;
        }
        public static MovieCarouselViewModelBuilder Init(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            return new MovieCarouselViewModelBuilder(unitOfWork, changeViewCommand);
        }

        public MovieCarouselViewModel Build()
        {
            return _movieCarouselViewModel;
        }
        private void LoadFilms(int count)
        {
            _unitOfWork.FilmRepository.GetRandomReleasedFilms(5).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _movieCarouselViewModel.Films = new ObservableCollection<Film>(task.Result);
                }
            });
        }
    }
}
