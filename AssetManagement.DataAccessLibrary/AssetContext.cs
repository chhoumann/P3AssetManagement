using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AssetManagement.DataAccessLibrary
{
    public sealed class AssetContext : DbContext
    {
        private readonly string connectionStringKey = "p3db";

        public DbSet<AssetData> AssetData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                string connectionString = configuration.GetConnectionString(connectionStringKey);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
