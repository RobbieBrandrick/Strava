using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Strava.Core.Models;

namespace Strava.Core.Data
{
    public class StravaDbContext : DbContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public StravaDbContext()
        {
            _connectionStrings = new ConnectionStrings()
            {
                StravaDb = "Data Source=strava.db"
            };
        }
        
        public StravaDbContext(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite(_connectionStrings.StravaDb);
            
        }
        
        public DbSet<StravaToken> Tokens { get; set; }
        public DbSet<StravaApiTransaction> StravaApiTransactions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        
    }
}