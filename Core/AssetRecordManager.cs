using System;
using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DbContexts;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;
using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;

namespace AssetManagement.Core
{
    // TODO: Should auto-update for assets when something happens to an asset (state changed / ownership, etc)
    public class AssetRecordManager
    {
        private ISqlDataAccess<AssetRecordData> dataAccess;

        public AssetRecordManager(ISqlDataAccess<AssetRecordData> dataAccess) => this.dataAccess = dataAccess;

        /// <summary>
        /// A method which subscribes to a status change in any asset.
        /// </summary>
        public AssetRecordManager StartWatchingForAssetStatusChange()
        {
            AssetOwnershipHandler.AssetStatusChanged += StatusChanged;
            return this;
        }

        /// <summary>
        /// A delegate function, which is excecuted every time a status is changed in an asset.
        /// </summary>
        /// <param name="asset">The asset in question.</param>
        /// <param name="state">The new state.</param>
        private async void StatusChanged(Asset asset, IAssetHolder newAssetHolder, AssetState state)
        {
            AssetRecord record = new AssetRecord(state, newAssetHolder, asset.AssetId);
            asset.AssetRecords.Add(record);
            
            await AddRecordToDatabase(record);
        }

        /// <summary>
        /// Adds an asset record to the database.
        /// </summary>
        /// <param name="record">The asset record in question.</param>
        /// <returns>Task</returns>
        private async Task AddRecordToDatabase(AssetRecord record)
        {
            AssetRecordData assetRecordData = new AssetRecordData(record);
            await dataAccess.Insert(assetRecordData);
            await dataAccess.Save();
        }
    }
}
