using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.WPF.State.Stores
{
    public class Store<T> : IStore<T> where T : DbObject
    {
        private readonly IGenericDataService<T> _dataService;
        public List<T> Entities { get; set; }

        public Store(ICollection<T> entities)
        {
            Entities = new List<T>(entities);
        }

        public Store(IGenericDataService<T> dataService)
        {
            _dataService = dataService;
        }

        public async Task Load()
        {
            Entities = new List<T>(await _dataService.GetAll());
        }

        public async Task Load(Func<T, bool> predicate)
        {
            Entities = new List<T>((await _dataService.GetAll()).Where(predicate));
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