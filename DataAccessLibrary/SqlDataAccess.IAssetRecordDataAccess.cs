using AssetManagement.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.DataAccessLibrary
{
    public sealed partial class SqlDataAccess
    {
        public class IAssetRecordDataAccess
        {
            private AssetContext Db { get; set; }

            public IAssetRecordDataAccess(AssetContext db) => Db = db;

            /// <summary>
            /// Adds a new asset record to the database.
            /// </summary>
            /// <param name="assetRecord">IAssetRecord to add to database.</param>
            public async Task CreateIAssetRecord(IAssetRecord assetRecord)
            {
                Console.WriteLine($"Executing Create query for ID {assetRecord.AssetId}");

                AssetRecordData assetRecordData = new AssetRecordData(assetRecord);

                await Db.AssetRecordData.AddAsync(assetRecordData);
                await Db.SaveChangesAsync();

                Console.WriteLine("Successfully created Asset Record:");
                Console.WriteLine(assetRecordData);
            }

            /// <summary>
            /// Reads and returns a single asset record from the database.
            /// </summary>
            /// <param name="assetId">ID for asset record we're looking for</param>
            /// <returns>An IAssetRecord, and throws exception if it's not found.</returns>
            public async Task<IAssetRecord> ReadSingleIAssetRecord(string assetId)
            {
                Console.WriteLine($"Executing ReadSingle query for ID {assetId}.");

                AssetRecordData assetRecord = await Db.AssetRecordData.SingleAsync(item => item.AssetId == assetId);

                Console.WriteLine("Query completed with following results:");
                Console.WriteLine(assetRecord);

                return assetRecord.ToIAssetRecord();
            }

            /// <summary>
            /// Reads all assets records from database and returns them
            /// </summary>
            /// <returns>Array of all assets records in database</returns>
            public async Task<IAssetRecord[]> ReadAllIAssetRecords()
            {
                Console.WriteLine("Executing ReadAll query.");

                List<AssetRecordData> assetRecords = await Db.AssetRecordData.ToListAsync();
                IAssetRecord[] result = new IAssetRecord[assetRecords.Count];

                Console.WriteLine("Query completed.");

                for (int i = 0; i < assetRecords.Count; i++)
                {
                    result[i] = assetRecords[i].ToIAssetRecord();
                }

                return result;
            }

            /// <summary>
            /// Updates a given asset record to the asset record passed in.
            /// </summary>
            /// <param name="assetRecord"></param>
            public async Task UpdateIAssetRecord(IAssetRecord assetRecord)
            {
                Console.WriteLine($"Executing Update query for ID {assetRecord.AssetId}.");

                Db.Update(new AssetRecordData(assetRecord));

                await Db.SaveChangesAsync();

                Console.WriteLine("Asset record successfully updated.");
            }

            /// <summary>
            /// Deletes passed in asset record.
            /// </summary>
            /// <param name="assetRecord">Asset record to delete</param>
            public async Task DeleteIAssetRecord(IAssetRecord assetRecord)
            {
                Console.WriteLine($"Executing DeleteSingle query for ID {assetRecord.AssetId}.");

                AssetRecordData dbItem = await Db.AssetRecordData.SingleAsync(item => item.AssetId == assetRecord.AssetId);

                Db.Remove(dbItem);

                await Db.SaveChangesAsync();

                Console.WriteLine("Asset record successfully removed.");
            }
        }
    }
}
