using Microsoft.EntityFrameworkCore;
using WormsApplication.data.behavior.entity;

namespace WormsApplication.data
{
    public class BehaviorContext : DbContext
    {
        public BehaviorContext(DbContextOptions<BehaviorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Behaviors> Behaviors { get; set; }
    }
}