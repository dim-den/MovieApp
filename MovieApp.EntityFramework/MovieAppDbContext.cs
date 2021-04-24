﻿using System;
using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Models;

namespace MovieApp.EntityFramework
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmReview> FilmReviews { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmCast> FilmCasts { get; set; }
        public object Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieAppDB;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
