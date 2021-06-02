using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieApp.Domain.Models;

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