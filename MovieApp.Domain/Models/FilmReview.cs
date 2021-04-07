using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public class FilmReview : DbObject
    {
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Film Film { get; set; }
        public User User { get; set; }
    }
}
