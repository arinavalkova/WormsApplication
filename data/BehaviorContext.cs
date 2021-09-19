using Microsoft.EntityFrameworkCore;
using WormsApplication.data.behavior.entity;

namespace WormsApplication.data
{
    public class BehaviorContext : DbContext
    {
        public DbSet<Behaviors> Behaviors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Behaviors");
        }
    }
}