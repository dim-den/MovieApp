using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.AuthenticationServices;

namespace MovieApp.WPF.State.Authentificator
{
    public interface IAuthenticator
    {
        User CurrentUser { get; set; }
        bool IsLoggedIn { get; }

        event Action StateChanged;
        Task Register(string username, string email, string password, string confirmPassword, string name, string surname);
        Task Login(string username, string password);
        void Logout();
    }
}
