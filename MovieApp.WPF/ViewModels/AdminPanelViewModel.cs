using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class AdminPanelViewModel : ViewModelBase
    {
        public MessageViewModel ErrorMessageViewModel { get; }
        public AsyncCommandBase DeleteEntityCommand { get;  }
        public AsyncCommandBase AddEntityCommand { get;  }
        public AsyncCommandBase UpdateEntityCommand { get;  }
        public BrowseImageCommand BrowseImageCommand { get;  }

        private readonly IUnitOfWork _unitOfWork;

        private readonly ObservableCollection<User> _users;
        private readonly ObservableCollection<FilmReview> _filmReviews;
        private readonly ObservableCollection<Film> _films;
        private readonly ObservableCollection<FilmCast> _filmCasts;
        private readonly ObservableCollection<Actor> _actors;

        private string _deleteEntityID;
        public string DeleteEntityID
        {
            get => _deleteEntityID;
            set
            {
                _deleteEntityID = value;
                OnPropertyChanged(nameof(DeleteEntityID));
            }
        }
        
        private string _updateEntityID;
        public string UpdateEntityID
        {
            get => _updateEntityID;
            set
            {
                _updateEntityID = value;
                OnPropertyChanged(nameof(UpdateEntityID));
            }
        }
        public User User { get; set; } = new User();
        public Film Film { get; set; } = new Film();
        public FilmReview FilmReview { get; set; } = new FilmReview();
        public FilmCast FilmCast { get; set; } = new FilmCast();
        public Actor Actor { get; set; } = new Actor();
       
        public ObservableCollection<User> Users => _users;
        public ObservableCollection<FilmReview> Reviews => _filmReviews;
        public ObservableCollection<Film> Films => _films;
        public ObservableCollection<FilmCast> Casts => _filmCasts;
        public ObservableCollection<Actor> Actors => _actors;       
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public AdminPanelViewModel(IAuthenticator authentificator)
        {
            _unitOfWork = new UnitOfWork();

            DeleteEntityCommand = new DeleteEnitityCommand(this, _unitOfWork, authentificator);
            UpdateEntityCommand = new UpdateEntityCommand(this, _unitOfWork, authentificator);
            BrowseImageCommand = new BrowseImageCommand(this);
            AddEntityCommand = new AddEntityCommand(this, _unitOfWork);

            ErrorMessageViewModel = new MessageViewModel();

            _users = new ObservableCollection<User>(_unitOfWork.UserRepository.GetAllSync());
            _filmReviews = new ObservableCollection<FilmReview>(_unitOfWork.FilmReviewRepository.GetAllSync());
            _films = new ObservableCollection<Film>(_unitOfWork.FilmRepository.GetAllSync());
            _filmCasts = new ObservableCollection<FilmCast>(_unitOfWork.FilmCastRepository.GetAllSync());
            _actors = new ObservableCollection<Actor>(_unitOfWork.ActorRepository.GetAllSync());
        }

    }
}
