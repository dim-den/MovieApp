using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.State.Stores
{
    public interface IStore<T> where T : DbObject
    {
        List<T> Entities { get; set; }

        Task Load();

        Task Load(Func<T, bool> predicate);

        Task LoadWithInclude(params Expression<Func<T, object>>[] includeProperties);

        Task LoadWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}