﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class FilmViewModel : ViewModelBase
    {
        public UserReviewsPanelViewModel UserReviewsPanel { get; }
        public RateFilmPanelViewModel RateFilmPanel { get; }

        private readonly INavigator _navigator;
        private readonly IAuthenticator _authentificator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStore<FilmCast> _filmCastStore;
        private readonly IStore<FilmReview> _filmReviewStore;
    
        public Film Film { get;  }

        public double FilmAvgScore => _unitOfWork.FilmReviewRepository.GetFilmAvgScore(Film.ID);

        public int FilmReviewsCount => _unitOfWork.FilmReviewRepository.GetFilmReviewsCount(Film.ID);

        private FilmReview _currentUserFilmReview;
        public FilmReview CurrentUserFilmReview
        {
            get => _currentUserFilmReview;
            set
            {
                _currentUserFilmReview = value;
                OnPropertyChanged(nameof(CurrentUserFilmReview));
                OnPropertyChanged(nameof(CurrentUserScoredFilm));
                OnPropertyChanged(nameof(FilmReviewsCount));
                OnPropertyChanged(nameof(FilmAvgScore));
            }
        }

        public bool CurrentUserScoredFilm => _currentUserFilmReview != null;

        public List<FilmCast> Cast => _filmCastStore.Entities;

        public List<FilmReview> Reviews => _filmReviewStore.Entities;

        public FilmViewModel(INavigator navigator, IAuthenticator authentificator, 
                             IStore<FilmCast> filmCastStore, IStore<FilmReview> filmReviewStore,  Film film, FilmReview CurrentUserFilmReview)
        {
            _unitOfWork = new UnitOfWork();
            _navigator = navigator;
            _authentificator = authentificator;
            _filmCastStore = filmCastStore;
            _filmReviewStore = filmReviewStore;
            _currentUserFilmReview = CurrentUserFilmReview;
            Film = film;

            UserReviewsPanel = new UserReviewsPanelViewModel(_currentUserFilmReview, film, authentificator, _unitOfWork, filmReviewStore);
            RateFilmPanel = new RateFilmPanelViewModel(_currentUserFilmReview, film, authentificator, _unitOfWork);

            UserReviewsPanel.PropertyChanged += UserReview_PropertyChanged;
            RateFilmPanel.PropertyChanged += UserReview_PropertyChanged;
        }

        private void UserReview_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserReviewsPanel.CurrentUserFilmReview) ||
                e.PropertyName == nameof(RateFilmPanel.CurrentUserFilmReview))
            {
                OnPropertyChanged(nameof(CurrentUserFilmReview));
                OnPropertyChanged(nameof(CurrentUserScoredFilm));
                OnPropertyChanged(nameof(FilmReviewsCount));
                OnPropertyChanged(nameof(FilmAvgScore));
            }
        }
    }
}