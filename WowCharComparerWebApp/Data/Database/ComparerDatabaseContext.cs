using Microsoft.EntityFrameworkCore;
using WowCharComparerWebApp.Data.Database.DbModels;
using WowCharComparerWebApp.Models.Achievement;

namespace WowCharComparerWebApp.Data.Database
{
    public class ComparerDatabaseContext : DbContext
    {
        // Database table which holds info about tokens
        public DbSet<OAuth2Token> OAuth2Tokens { get; set; }

        //Database table which holds user's information
        public DbSet<User> Users { get; set; }

        public DbSet<Achievements> Achievements { get; set; }

        private string ConnectionString { get; set; }

        public ComparerDatabaseContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
