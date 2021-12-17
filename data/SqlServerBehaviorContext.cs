using Microsoft.EntityFrameworkCore;
using WormsApplication.data.behavior.entity;

namespace WormsApplication.data
{
    public class SqlServerBehaviorContext : DbContext
    {
        public SqlServerBehaviorContext(DbContextOptions<SqlServerBehaviorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Behaviors> Behaviors { get; set; }
        public DbSet<Coords> Coords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Behaviors>().HasIndex(behavior => behavior.Name).IsUnique();
        }
    }
}