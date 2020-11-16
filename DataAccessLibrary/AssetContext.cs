using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using AssetManagement.Models.Asset;

namespace AssetManagement.DataAccessLibrary
{
    public sealed class AssetContext : DbContext
    {
        public DbSet<Asset> AssetData { get; set; }
        public DbSet<AssetRecordData> AssetRecordData { get; set; }

        private readonly string connectionStringKey = "p3db";

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
