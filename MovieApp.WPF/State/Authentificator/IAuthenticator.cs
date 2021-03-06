using System;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.State.Authentificator
{
    public interface IAuthenticator
    {
        User CurrentUser { get; set; }
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task Register(string username, string email, string password, string confirmPassword, string name, string surname);

        Task Login(string username, string password);

        Task ChangePassword(string oldPassword, string newPassword, string confirmPassword);

        void Logout();
    }
}