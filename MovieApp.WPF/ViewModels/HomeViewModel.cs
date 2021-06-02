namespace MovieApp.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private MovieCarouselViewModel _movieCarouselViewModel;

        public MovieCarouselViewModel MovieCarouselViewModel
        {
            get => _movieCarouselViewModel;
            set
            {
                _movieCarouselViewModel = value;
                OnPropertyChanged(nameof(MovieCarouselViewModel));
            }
        }

        private ActorsSummaryViewModel _actorsSummaryViewModel;

        public ActorsSummaryViewModel ActorsSummaryViewModel
        {
            get => _actorsSummaryViewModel;
            set
            {
                _actorsSummaryViewModel = value;
                OnPropertyChanged(nameof(ActorsSummaryViewModel));
            }
        }

        private UpcomingFilmsListViewModel _upcomingFilmsListViewModel;

        public UpcomingFilmsListViewModel UpcomingFilmsListViewModel
        {
            get => _upcomingFilmsListViewModel;
            set
            {
                _upcomingFilmsListViewModel = value;
                OnPropertyChanged(nameof(UpcomingFilmsListViewModel));
            }
        }

        public HomeViewModel()
        {
        }
    }
}