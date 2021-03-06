using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieApp.Domain.Services
{
    public interface IGenericDataService<T>
    {
        Task<ICollection<T>> GetAll();

        ICollection<T> GetAllSync();

        Task<ICollection<T>> GetRandomEntities(int randomEntitiesCount);

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);

        Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}