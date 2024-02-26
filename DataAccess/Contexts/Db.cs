using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class Db:DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserMovie> UserMovies { get; set; }

        public Db(DbContextOptions options) : base(options) // super in Java
        {

        }

        public Db()
        {

        }
    }
}
