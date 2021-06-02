using System;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.State.Authentificator
{
    public class Account
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
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}