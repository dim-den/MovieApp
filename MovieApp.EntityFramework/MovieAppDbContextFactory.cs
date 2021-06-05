using Microsoft.EntityFrameworkCore;

namespace MovieApp.EntityFramework
{
    public class MovieAppDbContextFactory
    {
        private readonly string _connectionString;

        public MovieAppDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MovieAppDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<MovieAppDbContext> options = new DbContextOptionsBuilder<MovieAppDbContext>();

            options.UseSqlServer(_connectionString);

            return new MovieAppDbContext(options.Options);
        }
    }
}