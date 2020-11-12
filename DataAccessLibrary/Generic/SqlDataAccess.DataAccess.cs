using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.DataAccessLibrary.Generic
{
    public interface IData
    {
        int Id { get; }
    }
    
    public sealed class SqlDataAccessGeneric<T> where T : IData
    {
        private ContextFactory<IData> Db { get; set; }

        public SqlDataAccessGeneric(ContextFactory<IData> db) => Db = db;

        /// <summary>
        /// Adds a new item to the database.
        /// </summary>
        /// <param name="item">Item to add to database</param>
        /// <returns>nothing</returns>
        public async Task Create(T item)
        {
            Console.WriteLine($"Executing Create query for ID {item}.");

            await Db.Data.AddAsync(item);
            await Db.SaveChangesAsync();

            Console.WriteLine("Successfully created Asset:");
            Console.WriteLine(item);
        }

        /// <summary>
        /// Reads and returns a single asset from the database.
        /// </summary>
        /// <param name="id">ID for asset we're looking for</param>
        /// <returns>an IAsset, and throws exception if it's not found.</returns>
        public async Task<IData> ReadSingleIAsset(int id)
        {
            Console.WriteLine($"Executing ReadSingle query for ID {id}.");

            IData data = await Db.Data.SingleAsync(item => item.Id == id);

            Console.WriteLine("Query completed with following results:");
            Console.WriteLine(data);

            return data;
        }

        /// <summary>
        /// Reads all assets from database and returns them
        /// </summary>
        /// <returns>IAsset array of all assets in database</returns>
        public async Task<IData[]> ReadAllData()
        {
            Console.WriteLine("Executing ReadAll query.");

            List<IData> data = await Db.Data.ToListAsync();

            Console.WriteLine("Query completed.");

            return data.ToArray();
        }

        /// <summary>
        /// Updates given data to the data passed in.
        /// </summary>
        /// <param name="data">The data to update.</param>
        public async Task UpdateIAsset(IData data)
        {
            Console.WriteLine($"Executing Update query for ID {data.Id}.");

            Db.Update(data);

            await Db.SaveChangesAsync();

            Console.WriteLine("Asset successfully updated.");
        }

        /// <summary>
        /// Deletes passed in data.
        /// </summary>
        /// <param name="data">Data to delete</param>
        public async Task DeleteIAsset(IData data)
        {
            Console.WriteLine($"Executing DeleteSingle query for ID {data.Id}.");

            IData dbItem = await Db.Data.SingleAsync(item => item.Id == data.Id);

            Db.Remove(dbItem);

            await Db.SaveChangesAsync();

            Console.WriteLine("Asset successfully removed.");
        }
    }
}