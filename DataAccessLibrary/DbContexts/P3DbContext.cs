using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AssetManagement.DataAccessLibrary.DbContexts
{
    public abstract class P3DbContext : DbContext
    {
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