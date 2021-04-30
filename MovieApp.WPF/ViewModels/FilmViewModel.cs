using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class FilmViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;
        public Film Film { get;  }

        public double FilmAvgScore => _unitOfWork.FilmReviewRepository.GetFilmAvgScore(Film.ID);
        public int FilmReviewsCount => _unitOfWork.FilmReviewRepository.GetFilmReviewsCount(Film.ID);

        public FilmViewModel(INavigator navigator, IAuthenticator authentificator, Film film)
        {
            _unitOfWork = new UnitOfWork();
            _navigator = navigator;
            _authenticator = authentificator;

            Film = film;
        }
    }
}
