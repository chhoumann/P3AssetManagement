using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;

namespace AssetManagement.Server
{
    public class AssetService
    {
        private readonly SqlDataAccess<Asset> assetDataAccess = new SqlDataAccess<Asset>(new AssetContext());

        /// <summary>
        /// Gets all assets in database
        /// </summary>
        /// <returns>An array of all Assets in database</returns>
        public async Task<Asset[]> GetAssetsAsync()
        {
            IEnumerable<Asset> assets = await assetDataAccess.GetAll();
            return assets.ToArray();
        }

        /// <summary>
        /// Gets details about single Asset.
        /// </summary>
        /// <param name="assetId">Asset ID to look for.</param>
        /// <returns>Asset object</returns>
        public async Task<Asset> GetSingleAssetAsync(int assetId) => await assetDataAccess.GetById(assetId);
    }
}
