using System.IO;
using AssetManagement.DataAccessLibrary.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AssetManagement.DataAccessLibrary.Contexts
{
    public abstract class AssetContext : DbContext
    {
        private readonly string connectionStringKey = "P3EFCoreDB";
        public DbSet<AssetHolder> AssetHolders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString(connectionStringKey);
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}