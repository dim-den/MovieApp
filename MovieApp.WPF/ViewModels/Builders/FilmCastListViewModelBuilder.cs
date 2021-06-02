using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class FilmCastListViewModelBuilder : IViewModelBuilder<FilmCastListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private FilmCastListViewModel _filmCastListViewModel;

        private FilmCastListViewModelBuilder(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _filmCastListViewModel = new FilmCastListViewModel(changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public FilmCastListViewModelBuilder SetFilm(Film film)
        {
            _filmCastListViewModel.Film = film;
            LoadFilmCast();

            return this;
        }

        public static FilmCastListViewModelBuilder Init(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            return new FilmCastListViewModelBuilder(unitOfWork, changeViewCommand);
        }

        public FilmCastListViewModel Build()
        {
            return _filmCastListViewModel;
        }

        public void LoadFilmCast()
        {
            _unitOfWork.FilmCastRepository.GetWithInclude(f => f.FilmID == _filmCastListViewModel.Film.ID, f => f.Actor).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _filmCastListViewModel.Cast = new ObservableCollection<FilmCast>(task.Result);
                }
            });
        }
    }
}