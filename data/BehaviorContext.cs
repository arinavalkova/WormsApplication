using Microsoft.EntityFrameworkCore;
using WormsApplication.data.entity;

namespace WormsApplication.data
{
    public class BehaviorContext : DbContext
    {
        public BehaviorContext(DbContextOptions<BehaviorContext> options)
            : base(options)
        { }
        public DbSet<Behavior> Behaviors { get; set; }
    }
}