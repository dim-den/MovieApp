using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.ViewModels
{
    public class AppHeaderViewModel : ViewModelBase
    {
        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(ImageData));
            }
        }
        public string Name => _currentUser?.Name ?? "";
        public byte[] ImageData => _currentUser?.ImageData ?? null;

        public AppHeaderViewModel()
        {
            _currentUser = new User();
        }
    }
}
