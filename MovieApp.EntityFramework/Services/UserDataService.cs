using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework;
using MovieApp.EntityFramework.Services.Common;

namespace MovieApp.Domain.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly NonQueryDataService<User> _nonQueryDataService;

        public UserDataService()
        {
            _nonQueryDataService = new NonQueryDataService<User>();
        }

        public async Task<User> Create(User entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<User> Get(int id)
        {
            using MovieAppDbContext context = new();
            return await context.Users
                .Include(u => u.FilmReviews)
                .FirstOrDefaultAsync((e) => e.ID == id);
        }

        public async Task<ICollection<User>> GetAll()
        {
            using MovieAppDbContext context = new();
            return await context.Users
                .Include(u => u.FilmReviews)
                .ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            using MovieAppDbContext context = new();
            return await context.Users
                .Include(u => u.FilmReviews)
                .FirstOrDefaultAsync((e) => e.Email == email);
        }

        public async Task<User> GetByUsername(string username)
        {
            using (MovieAppDbContext context = new())
            {
                return await context.Users.FirstOrDefaultAsync((e) => e.Username == username);
            }
        }

        public async Task<User> Update(int id, User entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
