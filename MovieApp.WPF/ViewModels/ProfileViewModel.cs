﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public AsyncCommandBase ChangeImageCommand { get; set; }
        
        private readonly IAuthenticator _authentificator;
        private readonly IStore<FilmReview> _filmReviewsStore;

        public User CurrentUser => _authentificator.CurrentUser;

        public byte[] ImageData => CurrentUser?.ImageData;

        public string Name => CurrentUser?.Name;

        public string Surname => CurrentUser?.Surname;

        public string Username => CurrentUser?.Username;

        public int FilmsWatched => CurrentUser?.FilmReviews?.Count ?? 0;

        public double AvgScore => CurrentUser?.FilmReviews?.Average(u => u.Score) ?? 0;

        public List<FilmReview> UserReviews => _filmReviewsStore.Entities;

        public ProfileViewModel(INavigator navigator, IAuthenticator authentificator, IStore<FilmReview> filmReviewsStore)
        {
            _authentificator = authentificator;
            _filmReviewsStore = filmReviewsStore;

            ChangeImageCommand = new ChangeImageCommand(_authentificator);

            _authentificator.StateChanged += Authenticator_StateChanged;
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(ImageData));
        }
    }
}
