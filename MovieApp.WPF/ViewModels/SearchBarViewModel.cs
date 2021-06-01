using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.WPF.Commands;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.ViewModels
{
    public class SearchBarViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }

        private ICollection<Film> _allFilms;
        private ICollection<Actor> _allActors;

        private ObservableCollection<Film> _films;
        private ObservableCollection<Actor> _actors;
        public ObservableCollection<Film> Films
        {
            get => _films;
            set
            {
                _films = value;
                OnPropertyChanged(nameof(Films));
            }
        }

        public ObservableCollection<Actor> Actors
        {
            get => _actors;
            set
            {
                _actors = value;
                OnPropertyChanged(nameof(Actors));
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                UpdateFilmList(_searchText.ToLower());
                UpdateActorList(_searchText.ToLower());
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private void UpdateFilmList(string toFind)
        {
            Films = string.IsNullOrEmpty(toFind) ?
                new ObservableCollection<Film>() :
                new ObservableCollection<Film>(_allFilms?
                .Where(f => f.Title.ToLower().Contains(toFind) ||
                            f.Genre.ToLower().Contains(toFind))
                .Take(6));
        }
        private void UpdateActorList(string toFind)
        {
            Actors = string.IsNullOrEmpty(toFind) ?
                new ObservableCollection<Actor>() :
                new ObservableCollection<Actor>(_allActors?
                .Where(a => a.Name.ToLower().Contains(toFind) ||
                            a.Surname.ToLower().Contains(toFind))
                .Take(4));
        }

        public SearchBarViewModel(ICollection<Film> films, ICollection<Actor> actors, ICommand changeViewCommand )
        {
            _allFilms = films;
            _allActors = actors;

            ChangeViewCommand = changeViewCommand;

            _films = new ObservableCollection<Film>();
            _actors = new ObservableCollection<Actor>();
        }
    }
}
