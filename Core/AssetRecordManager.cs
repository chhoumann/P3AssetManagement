using System;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;
using System.Threading.Tasks;

namespace AssetManagement.Core
{
    // TODO: Should auto-update for assets when something happens to an asset (state changed / ownership, etc)
    public class AssetRecordManager
    {
        private ISqlDataAccess<AssetRecordData> DataAccess { get; }

        /* TODO: Remove this constructor and replace it with something that is decoupled from DB,
           some kind of abstraction over DB access */
        public AssetRecordManager(ISqlDataAccess<AssetRecordData> dataAccess) => DataAccess = dataAccess;

        public AssetRecordManager() { }
        
        /// <summary>
        /// A method which subscribes to a status change in any asset.
        /// </summary>
        public AssetRecordManager StartWatchingForAssetStatusChange()
        {
            Asset.AssetChanged += StatusChanged;
            return this;
        }

        /// <summary>
        /// A delegate function, which is executed every time a status is changed in an asset.
        /// </summary>
        /// <param name="asset">The asset in question.</param>
        /// <param name="state">The new state.</param>
        private void StatusChanged(Asset asset, IAssetHolder assetHolder, AssetState state)
        {
            AssetRecord record = new AssetRecord(state, assetHolder, asset.AssetId);
            asset.AssetRecords.Add(record);
            
            // TODO: Save to database
            //await AddRecordToDatabase(record);
        }

        /// <summary>
        /// Adds an asset record to the database.
        /// </summary>
        /// <param name="record">The asset record in question.</param>
        /// <returns>Task</returns>
        private async Task AddRecordToDatabase(AssetRecord record)
        {
            if (string.IsNullOrEmpty(record.AssetId)) return;
            AssetRecordData assetRecordData = new AssetRecordData(record);

            await DataAccess.Insert(assetRecordData);
            await DataAccess.Save();
        }
    }
}
