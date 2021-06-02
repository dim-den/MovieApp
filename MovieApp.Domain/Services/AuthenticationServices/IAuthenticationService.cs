using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<User> Register(string username, string email, string password, string confirmPassword, string name, string surname);

        Task<User> Login(string username, string password);

        Task<User> ChangePassword(User user, string oldPassword, string newPassword, string confirmPassword);
    }
}