using Microsoft.EntityFrameworkCore;
using WowCharComparerWebApp.Models;
using WowCharComparerWebApp.Models.Internal;
using WowCharComparerWebApp.Models.Achievement;

namespace WowCharComparerWebApp.Data.Database
{
    public class ComparerDatabaseContext : DbContext
    {
        // Hold API client information, to generate proper bearer token at request
        public DbSet<APIClient> APIClient { get; set; }

        // Database table which holds user's information
        public DbSet<User> Users { get; set; }

        public DbSet<BonusStats> BonusStats { get; set; }

        public DbSet<AchievementCategory> AchievementCategory { get; set; }

        public DbSet<AchievementsData> AchievementsData { get; set; }

        private static string ConnectionString { get; set; }

        public ComparerDatabaseContext() : base()
        {
        }

        public ComparerDatabaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BonusStats>().HasIndex(x => x.ID).IsUnique();
            modelBuilder.Entity<AchievementCategory>().HasIndex(x => x.ID).IsUnique();
            modelBuilder.Entity<AchievementsData>().HasIndex(x => x.ID).IsUnique();
        }
    }
}
