using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using WowCharComparerWebApp.Data.Database.DbModels;

namespace WowCharComparerWebApp.Data.Database
{
    public class ComparerDatabaseContext : DbContext
    {
        // Database table which holds info about tokens
        public DbSet<OAuth2Token> OAuth2Tokens { get; set; }

        // Database table which holds user's information
        public DbSet<User> Users { get; set; }

        private string connectionString { get; set; }

        public ComparerDatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
