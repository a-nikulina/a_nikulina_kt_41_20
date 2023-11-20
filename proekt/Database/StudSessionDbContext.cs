using Microsoft.EntityFrameworkCore;
using proekt.Database.Configurations;
using proekt.Models;
using System.Diagnostics;

using static System.Net.Mime.MediaTypeNames;

namespace proekt.Database
{
    public class StudSessionDbContext : DbContext
    {
        public DbSet<StudSession> StudSession { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new StudSessionConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
        }

        public StudSessionDbContext(DbContextOptions<StudSessionDbContext> options) : base(options)
        {

        }
    }
}

