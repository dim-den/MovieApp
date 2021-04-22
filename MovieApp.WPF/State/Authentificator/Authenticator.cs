using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.AuthenticationServices;

namespace MovieApp.WPF.State.Authentificator
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Account _account;
        public Authenticator(IAuthenticationService authenticationService, Account account)
        {
            _authenticationService = authenticationService;
            _account = account;
        }
        public User CurrentUser
        {
            get
            {
                return _account.CurrentUser;
            }
            set
            {
                _account.CurrentUser = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;

        public event Action StateChanged;

        public async Task Login(string username, string password)
        {
            CurrentUser = await _authenticationService.Login(username, password);
        }

        public void Logout()
        {
            CurrentUser = null; 
        }

        public async Task Register(string username, string email, string password, string confirmPassword, string name, string surname)
        {
           CurrentUser = await _authenticationService.Register(username, email, password, confirmPassword, name, surname);
        }

    }
}
