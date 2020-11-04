using AssetManagement.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.DataAccessLibrary
{
    public sealed class SqlDataAccess
    {
        private AssetContext Db { get; set; }

        public SqlDataAccess(AssetContext db) => Db = db;

        /// <summary>
        /// Adds a new asset to the database.
        /// </summary>
        /// <param name="asset">IAsset to add to database</param>
        /// <returns>nothing</returns>
        public async Task Create(IAsset asset)
        {
            Console.WriteLine($"Executing Create query for ID {asset.Id}.");

            AssetData assetData = new AssetData(asset);

            await Db.AssetData.AddAsync(assetData);
            await Db.SaveChangesAsync();

            Console.WriteLine("Successfully created Asset:");
            Console.WriteLine(assetData);
        }

        /// <summary>
        /// Reads and returns a single asset from the database.
        /// </summary>
        /// <param name="id">ID for asset we're looking for</param>
        /// <returns>an IAsset, and throws exception if it's not found.</returns>
        public async Task<IAsset> ReadSingle(int id)
        {
            Console.WriteLine($"Executing ReadSingle query for ID {id}.");
            
            AssetData asset = await Db.AssetData.SingleAsync(item => item.Id == id);

            Console.WriteLine("Query completed with following results:");
            Console.WriteLine(asset);

            return asset.ToIAsset();
        }

        /// <summary>
        /// Reads all assets from database and returns them
        /// </summary>
        /// <returns>IAsset array of all assets in database</returns>
        public async Task<IAsset[]> ReadAll()
        {
            Console.WriteLine("Executing ReadAll query.");

            List<AssetData> assets = await Db.AssetData.ToListAsync();
            IAsset[] result = new IAsset[assets.Count];

            Console.WriteLine("Query completed.");

            for (int i = 0; i < assets.Count; i++)
            {
                result[i] = assets[i].ToIAsset();
            }

            return result;
        }

        /// <summary>
        /// Updates a given asset to the asset passed in.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>Nothing</returns>
        public async Task Update(IAsset asset)
        {
            Console.WriteLine($"Executing Update query for ID {asset.Id}.");

            Db.Update(new AssetData(asset));
            
            await Db.SaveChangesAsync();

            Console.WriteLine("Asset successfully updated.");
        }

        /// <summary>
        /// Deletes passed in asset.
        /// </summary>
        /// <param name="asset">asset to delete</param>
        /// <returns></returns>
        public async Task Delete(IAsset asset)
        {
            Console.WriteLine($"Executing DeleteSingle query for ID {asset.Id}.");

            AssetData dbItem = await Db.AssetData.SingleAsync(item => item.Id == asset.Id);

            Db.Remove(dbItem);

            await Db.SaveChangesAsync();

            Console.WriteLine("Asset successfully removed.");
        }
    }
}
