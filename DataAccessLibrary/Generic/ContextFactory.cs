using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AssetManagement.DataAccessLibrary.Generic
{
    public sealed class ContextFactory<T>: DbContext where T : class
    {
        public DbSet<T> Data { get; set; }

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