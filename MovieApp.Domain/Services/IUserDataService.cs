using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services
{
    public interface IUserDataService : IDataService<User>
    {
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
    }
}
