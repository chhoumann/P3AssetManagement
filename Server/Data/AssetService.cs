using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DbContexts;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;

namespace AssetManagement.Server
{
    public class AssetService
    {
        //private readonly SqlDataAccess<Asset> assetDataAccess = new SqlDataAccess<Asset>(new AssetContext());
        public event Action AssetUpdated;

        // TODO:
        // Remove this and add modal for adding assets.
        private int idTracker = 0;
        
        /// <summary>
        /// Gets all assets in database
        /// </summary>
        /// <returns>An array of all Assets in database</returns>
        public async Task<ComputerAsset[]> GetAssetsAsync()
        { 
            SqlDataAccess<ComputerAsset> assetDataAccess = new SqlDataAccess<ComputerAsset>(new AssetContext());
            IEnumerable<ComputerAsset> assets = await assetDataAccess.GetAll();

            // idTracker = GetNextId(assets);
            
            return assets.ToArray();
        }

        /// <summary>
        /// Gets details about single Asset.
        /// </summary>
        /// <param name="assetId">Asset ID to look for.</param>
        /// <returns>Asset object</returns>
        public async Task<ComputerAsset> GetSingleAssetAsync(string assetId)
        {
            SqlDataAccess<ComputerAsset> assetDataAccess = new SqlDataAccess<ComputerAsset>(new AssetContext());

            return await assetDataAccess.GetById(assetId);
        }
        
        public async Task CreateNewAsset()
        {
            SqlDataAccess<ComputerAsset> assetDataAccess = new SqlDataAccess<ComputerAsset>(new AssetContext());
            ComputerAsset asset = new ComputerAsset($"Computer {idTracker++}", $"SN0931{idTracker}") { AssetId = "TEST_ID"};
            
            await assetDataAccess.Insert(asset);
            await assetDataAccess.Save();
            
            AssetUpdated?.Invoke();
        }

        public async Task DeleteAsset(string id)
        {
            SqlDataAccess<ComputerAsset> assetDataAccess = new SqlDataAccess<ComputerAsset>(new AssetContext());
            await assetDataAccess.Delete(id);
            await assetDataAccess.Save();
            
            AssetUpdated?.Invoke();
        }
    }
}
