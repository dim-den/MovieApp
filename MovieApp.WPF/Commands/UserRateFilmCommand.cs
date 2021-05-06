using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class UserRateFilmCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IAuthenticator _authenticator;
        private readonly ILeaveReviewService _leaveReviewService;
        private readonly RateFilmPanelViewModel _rateFilmPanelViewModel;

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            int score = Convert.ToInt32((string) parameter);

            var review = await _leaveReviewService.LeaveScore(_rateFilmPanelViewModel.Film,
                                                              _authenticator.CurrentUser,
                                                              score);

            _rateFilmPanelViewModel.CurrentUserFilmReview = review;
        }

        public UserRateFilmCommand(RateFilmPanelViewModel rateFilmPanelViewModel, IAuthenticator authenticator, ILeaveReviewService leaveReviewService)
        {
            _rateFilmPanelViewModel = rateFilmPanelViewModel;
            _leaveReviewService = leaveReviewService;
            _authenticator = authenticator;
        }
    }
}
