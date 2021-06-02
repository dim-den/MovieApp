using System;
using System.Collections.Generic;

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