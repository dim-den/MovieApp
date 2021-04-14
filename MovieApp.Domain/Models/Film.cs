using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public class Film : DbObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Budget { get; set; }
        public int Fees { get; set; }
        public byte[] PosterImageData { get; set; }
        public ICollection<FilmReview> FilmReviews { get; set; }
        public ICollection<FilmCast> FilmCasts { get; set; }
    }
}
