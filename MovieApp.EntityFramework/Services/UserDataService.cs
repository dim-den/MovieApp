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
    public class UserDataService : IUserDataService
    {
        private readonly GenericDataService<User> _userDataService;

        public UserDataService()
        {
            _userDataService = new GenericDataService<User>();
        }

        public async Task<User> Create(User entity)
        {
            return await _userDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _userDataService.Delete(id);
        }

        public async Task<User> Get(int id)
        {
            return await _userDataService.Get(id);
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _userDataService.GetAll();
        }

        public ICollection<User> GetAllSync()
        {
            return _userDataService.GetAllSync();
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
            using MovieAppDbContext context = new();
            return await context.Users.FirstOrDefaultAsync((e) => e.Username == username);
        }

        public async Task<IEnumerable<User>> GetWithInclude(params Expression<Func<User, object>>[] includeProperties)
        {
            return await _userDataService.GetWithInclude(includeProperties);
        }

        public async Task<IEnumerable<User>> GetWithInclude(Func<User, bool> predicate, params Expression<Func<User, object>>[] includeProperties)
        {
            return await _userDataService.GetWithInclude(predicate, includeProperties);
        }

        public async Task<User> Update(int id, User entity)
        {
            return await _userDataService.Update(id, entity);
        }
    }
}
