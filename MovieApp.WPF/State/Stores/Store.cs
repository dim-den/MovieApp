using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services.FilmServices;
using MovieApp.EntityFramework.Services;

namespace MovieApp.WPF.State.Stores
{
    public class Store<T> : IStore<T> where T : DbObject
    {
        private readonly GenericDataService<T> _dataService;
        public List<T> Entities { get; set; }
        public Store()
        {
            _dataService = new GenericDataService<T>();
        }

        public async Task Load()
        {
            Entities = new List<T>(await _dataService.GetAll());
        }
       
        public async Task LoadWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            Entities = new List<T>(await _dataService.GetWithInclude(includeProperties));
        }

        public async Task LoadWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            Entities = new List<T>(await _dataService.GetWithInclude(predicate, includeProperties));
        }
    }
}
