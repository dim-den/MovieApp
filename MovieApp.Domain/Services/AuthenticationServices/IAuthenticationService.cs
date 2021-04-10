using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<User> Register(string username, string email, string password, string confirmPassword, string name, string surname);
        Task<User> Login(string username, string password);
    }
}
