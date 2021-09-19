using Microsoft.EntityFrameworkCore;
using WormsApplication.data.behavior.entity;

namespace WormsApplication.data
{
    public class SqlServerBehaviorContext : DbContext
    {
        private string _connectionString;

        public SqlServerBehaviorContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        public DbSet<Behaviors> Behaviors { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}