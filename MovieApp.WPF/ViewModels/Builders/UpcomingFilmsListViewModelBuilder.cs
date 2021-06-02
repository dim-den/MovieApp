using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class UpcomingFilmsListViewModelBuilder : IViewModelBuilder<UpcomingFilmsListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private UpcomingFilmsListViewModel _upcomingFilmsListViewModel;

        private UpcomingFilmsListViewModelBuilder(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            _upcomingFilmsListViewModel = new UpcomingFilmsListViewModel(changeViewCommand);
            _unitOfWork = unitOfWork;
        }

        public UpcomingFilmsListViewModelBuilder SetFilmCount(int count)
        {
            LoadFilms(count);

            return this;
        }

        public static UpcomingFilmsListViewModelBuilder Init(IUnitOfWork unitOfWork, ICommand changeViewCommand)
        {
            return new UpcomingFilmsListViewModelBuilder(unitOfWork, changeViewCommand);
        }

        public UpcomingFilmsListViewModel Build()
        {
            return _upcomingFilmsListViewModel;
        }

        private void LoadFilms(int count)
        {
            _unitOfWork.FilmRepository.GetUpcomingFilms().ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    _upcomingFilmsListViewModel.UpcomingFilms = new ObservableCollection<Film>(task.Result.Take(count));
                }
            });
        }
    }
}