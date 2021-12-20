using Microsoft.EntityFrameworkCore;
using WormsApplication.data.behavior.entity;

namespace WormsApplication.data.behavior.contexts
{
    public class BehaviorContext : DbContext
    {
        public DbSet<Behaviors> Behaviors { get; set; }
        
        public DbSet<Coords> Coords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Behaviors");
        }
    }
}