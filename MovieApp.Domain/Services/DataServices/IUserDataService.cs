using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services
{
    public interface IUserDataService : IGenericDataService<User>
    {
        Task<User> GetByUsername(string username);

        Task<User> GetByEmail(string email);
    }
}