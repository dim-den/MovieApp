using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.EntityFramework
{
    public class MovieAppDbContextFactory
    {
        private readonly string _connectionString;

        public MovieAppDbContextFactory(bool remoteMode, string connectionString)
        {
            MovieAppDbContext.REMOTE_MODE = remoteMode;
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
