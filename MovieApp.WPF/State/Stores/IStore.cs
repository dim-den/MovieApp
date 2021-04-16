using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.State.Stores
{
    public interface IStore<T> where T : DbObject
    {
        List<T> Entities { get; set; }
        public Task Load();
        public Task LoadWithInclude(params Expression<Func<T, object>>[] includeProperties);
        public Task LoadWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
