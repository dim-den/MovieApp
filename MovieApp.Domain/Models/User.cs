using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public enum ClientType
    {
        User,
        Admin
    }
    public class User : DbObject
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public ClientType ClientType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
        public byte[] ImageData { get; set; }
        public ICollection<FilmReview> FilmReviews { get; set; }
    }
}
