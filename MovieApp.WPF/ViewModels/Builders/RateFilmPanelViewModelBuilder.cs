using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.ViewModels.Builders
{
    public class RateFilmPanelViewModelBuilder : IViewModelBuilder<RateFilmPanelViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private RateFilmPanelViewModel _rateFilmPanelViewModel;

        private RateFilmPanelViewModelBuilder(IAuthenticator authenticator, IUnitOfWork unitOfWork, ILeaveReviewService leaveReviewService)
        {
            _rateFilmPanelViewModel = new RateFilmPanelViewModel(authenticator, unitOfWork, leaveReviewService);
            _unitOfWork = unitOfWork;
        }

        public RateFilmPanelViewModelBuilder SetFilm(Film film)
        {
            _rateFilmPanelViewModel.Film = film;

            return this;
        }

        public static RateFilmPanelViewModelBuilder Init(IAuthenticator authenticator, IUnitOfWork unitOfWork, ILeaveReviewService leaveReviewService)
        {
            return new RateFilmPanelViewModelBuilder(authenticator, unitOfWork, leaveReviewService);
        }

        public RateFilmPanelViewModel Build()
        {
            return _rateFilmPanelViewModel;
        }
    }
}