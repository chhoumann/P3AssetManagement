#define EXECUTESAVE
#define EXECUTEQUERYALL
#define EXECUTEQUERYSINGLE
#define DELETESINGLE

using AssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.DataAccessLibrary
{
    public sealed class SqlDataAccess
    {
        public void SampleDatabaseOperations()
        {
#if EXECUTESAVE
            /* EXAMPLE: Save to database */
            using (AssetContext db = new AssetContext())
            {
                // Create a new asset and save it to the database
                IAsset asset = AssetController.MakeAsset(100, "Gamer", "100");
                AssetData assetData = new AssetData(asset);

                Console.WriteLine(asset.LastChanged);

                Console.WriteLine(assetData);
                Console.WriteLine("----------------------");
                Console.WriteLine(db.AssetData);

                db.AssetData.Add(assetData);
                Console.WriteLine("Calling SaveChanges.");
                db.SaveChanges();
                Console.WriteLine("SaveChanges completed.");
            }
#endif
#if EXECUTEQUERYALL
            /* EXAMPLE: Get all elements in database */
            using (AssetContext db = new AssetContext())
            {
                // Query for all assets
                Console.WriteLine("Executing query.");

                List<AssetData> assets = db.AssetData.ToList();

                // Write all blogs out to Console
                Console.WriteLine("Query completed with following results:");

                foreach (var asset in assets)
                {
                    Console.WriteLine(" " + asset.Id);
                }
            }
#endif
#if EXECUTEQUERYSINGLE
            /* EXAMPLE: Get single element in database */
            using (AssetContext db = new AssetContext())
            {
                Console.WriteLine("Executing query.");

                // Query for single asset with Id 100
                var asset = db.AssetData.Single(b => b.Id == 100);

                Console.WriteLine("Query completed with following results:");
                Console.WriteLine(asset);
            }
#endif
#if DELETESINGLE
            /* EXAMPLE: Delete single element in database */
            using (AssetContext db = new AssetContext())
            {
                Console.WriteLine("Executing query.");

                // Query for single asset with Id 100
                var asset = db.AssetData.Single(b => b.Id == 100);
                db.Remove(asset);
                db.SaveChanges();

                Console.WriteLine("Asset removed.");
            }
#endif
        }
    }
}
