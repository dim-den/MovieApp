using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Stores;

namespace MovieApp.WPF.ViewModels
{
    public class ActorsSummaryViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; }

        private ObservableCollection<Actor> _actors;
        public ObservableCollection<Actor> Actors
        {
            get => _actors;
            set
            {
                _actors = value;
                OnPropertyChanged(nameof(Actors));
            }
        }
        public ActorsSummaryViewModel(ICommand changeViewCommand)
        {
            ChangeViewCommand = changeViewCommand;
        }
    }
}
