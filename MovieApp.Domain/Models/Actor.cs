using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public class Actor : DbObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public DateTime Bday { get; set; }
        public byte[] ImageData { get; set; }
        public ICollection<FilmCast> FilmCasts { get; set; }
    }
}
