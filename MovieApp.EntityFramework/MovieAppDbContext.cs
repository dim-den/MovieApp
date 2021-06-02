using System.Net;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;

namespace MovieApp.EntityFramework
{
    public class MovieAppDbContext : DbContext
    {
        public static bool REMOTE_MODE;
        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmReview> FilmReviews { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmCast> FilmCasts { get; set; }

        public MovieAppDbContext(DbContextOptions options) : base(options) { }

    }
}
