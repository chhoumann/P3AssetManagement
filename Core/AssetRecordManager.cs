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

        public AssetRecordManager(ISqlDataAccess<AssetRecordData> dataAccess) => DataAccess = dataAccess;

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

            // TODO: This line is what's making the entire program crash.
            // It seems as if it's trying to add 'null' entities to the database - which it obviously doesn't like.
            await AddRecordToDatabase(record);
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
