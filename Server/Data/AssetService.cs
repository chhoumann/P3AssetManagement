using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public async Task<Asset[]> GetAssetsAsync()
        { 
            SqlDataAccess<Asset> assetDataAccess = new SqlDataAccess<Asset>(new AssetContext());
            IEnumerable<Asset> assets = await assetDataAccess.GetAll();

            idTracker = GetNextId(assets);
            
            return assets.ToArray();
        }

        /// <summary>
        /// Gets the next possible asset ID.
        /// </summary>
        /// <param name="assets"></param>
        /// <returns>The next possible ID for an asset.</returns>
        private int GetNextId(IEnumerable<Asset> assets)
        {
            int next = 0;
            
            foreach (Asset asset in assets)
            {
                if (asset.Id > next)
                {
                    next = asset.Id + 1;
                }
            }

            return next;
        }

        /// <summary>
        /// Gets details about single Asset.
        /// </summary>
        /// <param name="assetId">Asset ID to look for.</param>
        /// <returns>Asset object</returns>
        public async Task<Asset> GetSingleAssetAsync(int assetId)
        {
            SqlDataAccess<Asset> assetDataAccess = new SqlDataAccess<Asset>(new AssetContext()); 
            
            return await assetDataAccess.GetById(assetId);
        }
        
        public async Task CreateNewAsset()
        {
            SqlDataAccess<Asset> assetDataAccess = new SqlDataAccess<Asset>(new AssetContext());
            Asset asset = new Asset(idTracker += 10, $"Computer {idTracker / 10}", $"SN0931{idTracker}");
            await assetDataAccess.Insert(asset);
            await assetDataAccess.Save();
            
            AssetUpdated?.Invoke();
        }

        public async Task DeleteAsset(int id)
        {
            SqlDataAccess<Asset> assetDataAccess = new SqlDataAccess<Asset>(new AssetContext());
            await assetDataAccess.Delete(id);
            await assetDataAccess.Save();
            
            AssetUpdated?.Invoke();
        }
    }
}
