using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DbObject
    {
        public async Task<T> Create(T entity)
        {
            using MovieAppDbContext context = new();
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            using MovieAppDbContext context = new();
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.ID == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
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

        public ICollection<T> GetAllSync()
        {
            using MovieAppDbContext context = new();
            return context.Set<T>().AsNoTracking().ToList();
        }

        public async Task<T> Update(int id, T entity)
        {
            using MovieAppDbContext context = new();
            entity.ID = id;

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

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
            MovieAppDbContext context = new();
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

    }
}