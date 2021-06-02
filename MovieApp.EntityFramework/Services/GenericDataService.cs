using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class GenericDataService<T> : IGenericDataService<T> where T : DbObject
    {
        protected readonly MovieAppDbContext _movieAppDbContext;
        private DbSet<T> _dbSet;
        protected DbSet<T> DbSet => _dbSet ?? (_dbSet = _movieAppDbContext.Set<T>());

        public GenericDataService(MovieAppDbContext movieAppDbContext)
        {
            _movieAppDbContext = movieAppDbContext;
        }

        public async Task<T> Create(T entity)
        {
            EntityEntry<T> createdResult = await DbSet.AddAsync(entity);

            return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            T entity = await DbSet.FirstOrDefaultAsync((e) => e.ID == id);
            DbSet.Remove(entity);

            return true;
        }

        public async Task<T> Get(int id)
        {
            T entity = await DbSet.FirstOrDefaultAsync((e) => e.ID == id);

            return entity;
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<T>> GetRandomEntities(int randomEntitiesCount)
        {
            return await DbSet.OrderBy(r => Guid.NewGuid()).Take(randomEntitiesCount).ToListAsync();
        }

        public ICollection<T> GetAllSync()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public async Task<T> Update(int id, T entity)
        {
            entity.ID = id;
            DbSet.Update(entity);

            return entity;
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
            IQueryable<T> query = DbSet.AsNoTracking();

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}