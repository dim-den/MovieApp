using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.State.Stores
{
    public interface IFilmStore
    {
        public List<Film> Films { get; set; }
    }
}
