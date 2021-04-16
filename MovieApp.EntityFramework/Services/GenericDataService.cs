using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.EntityFramework.Services.Common;

namespace MovieApp.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DbObject
    {
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService()
        {
            _nonQueryDataService = new NonQueryDataService<T>();
        }
        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<T> Get(int id)
        {
            using MovieAppDbContext context = new MovieAppDbContext();
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.ID == id);
            return entity;
        }

        public async Task<ICollection<T>> GetAll()
        {
            using MovieAppDbContext context = new();
            ICollection<T> entities = await context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return (await query.ToListAsync()).Where(predicate);
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            MovieAppDbContext context = new();

            IQueryable<T> query = context.Set<T>().AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}