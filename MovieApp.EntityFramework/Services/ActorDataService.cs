using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;

namespace MovieApp.EntityFramework.Services
{
    public class ActorDataService : GenericDataService<Actor>, IActorDataService
    {
        public ActorDataService(MovieAppDbContext movieAppDbContext) : base(movieAppDbContext)
        {
        }
    }
}
