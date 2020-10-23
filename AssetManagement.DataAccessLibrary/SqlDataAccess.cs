using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using AssetManagement.Models;

namespace AssetManagement.DataAccessLibrary
{
    public class AssetContext : DbContext
    {
        public DbSet<IAsset> Assets { get; set; }  
    }

    public class SqlDataAccess 
    {
        private readonly IConfiguration config;

        public string ConnectionStringName { get; } = "p3db";

        public SqlDataAccess(IConfiguration config)
        {
            this.config = config;
        } 

        public void Test()
        {
            string connectionString = config.GetConnectionString(ConnectionStringName);

            using (var db = new AssetContext())
            {
                // Create a new blog and save it
                IAsset asset = AssetController.MakeAsset(69, "Gamer", "69");

                Console.WriteLine(asset);
                Console.WriteLine("----------------------");
                Console.WriteLine(db.Assets);

                db.Assets.Add(asset);
                Console.WriteLine("Calling SaveChanges.");
                db.SaveChanges();
                Console.WriteLine("SaveChanges completed.");

                // Query for all blogs ordered by name
                /*Console.WriteLine("Executing query.");
                var blogs = (from b in db.Assets
                             orderby b.Name
                             select b).ToList();

                // Write all blogs out to Console
                Console.WriteLine("Query completed with following results:");
                foreach (var blog in blogs)
                {
                    Console.WriteLine(" " + blog.Name);
                }*/
            }
        }
    }
}
