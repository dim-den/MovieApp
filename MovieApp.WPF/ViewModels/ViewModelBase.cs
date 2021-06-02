using System.ComponentModel;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public delegate TViewModel CreateViewModelWithParam<TViewModel, TModel>(TModel param)
                                                        where TViewModel : ViewModelBase where TModel : DbObject;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual void Dispose()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}