using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class AdminPanelViewModel : ViewModelBase
    {
        public MessageViewModel ErrorMessageViewModel { get; }
        public AsyncCommandBase DeleteEntityCommand { get;  }
        public IUnitOfWork UnitOfWork { get;  }

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
        public ObservableCollection<User> Users => _users;
        public ObservableCollection<FilmReview> Reviews => _filmReviews;
        public ObservableCollection<Film> Films => _films;
        public ObservableCollection<FilmCast> Casts => _filmCasts;
        public ObservableCollection<Actor> Actors => _actors;       
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public AdminPanelViewModel(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            DeleteEntityCommand = new DeleteEnitityCommand(this);
            ErrorMessageViewModel = new MessageViewModel();

            _users = new ObservableCollection<User>(unitOfWork.UserRepository.GetAllSync());
            _filmReviews = new ObservableCollection<FilmReview>(unitOfWork.FilmReviewRepository.GetAllSync());
            _films = new ObservableCollection<Film>(unitOfWork.FilmRepository.GetAllSync());
            _filmCasts = new ObservableCollection<FilmCast>(unitOfWork.FilmCastRepository.GetAllSync());
            _actors = new ObservableCollection<Actor>(unitOfWork.ActorRepository.GetAllSync());
        }

    }
}
