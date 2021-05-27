using System.Net;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;

namespace MovieApp.EntityFramework
{
    public class MovieAppDbContext : DbContext
    {
        public static bool REMOTE_MODE = true;

        private const string _remoteDbConnectionString = "Server=tcp:dimden.database.windows.net,1433;Initial Catalog=MovieAppDB;Persist Security Info=False;User ID=nickname;Password=password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private const string _localDbConnectionString = "data source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieAppDB;Integrated Security=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmReview> FilmReviews { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmCast> FilmCasts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(REMOTE_MODE ? _remoteDbConnectionString : _localDbConnectionString,
                                        options => options.EnableRetryOnFailure());

            base.OnConfiguring(optionsBuilder);
        }

    }
}
