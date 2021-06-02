using System.Collections.Generic;

namespace MovieApp.Domain.Models
{
    public enum ClientType
    {
        User,
        Admin,
        SuperAdmin
    }

    public class User : DbObject
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }
        public ClientType ClientType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] ImageData { get; set; }
        public ICollection<FilmReview> FilmReviews { get; set; }
    }
}