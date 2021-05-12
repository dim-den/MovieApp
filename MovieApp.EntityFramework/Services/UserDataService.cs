using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework;
using MovieApp.EntityFramework.Services;

namespace MovieApp.Domain.Services
{
    public class UserDataService : GenericDataService<User>, IUserDataService
    {
        public UserDataService(MovieAppDbContext movieAppDbContext) : base(movieAppDbContext)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _movieAppDbContext.Users
                .Include(u => u.FilmReviews)
                .FirstOrDefaultAsync((e) => e.Email == email);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _movieAppDbContext.Users
                .Include(u => u.FilmReviews)
                .FirstOrDefaultAsync((e) => e.Username == username);
        }
    }
}
