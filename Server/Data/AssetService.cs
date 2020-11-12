using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary;
using AssetManagement.Models.Asset;

namespace AssetManagement.Server
{
    public class AssetService
    {
        private readonly SqlDataAccess dataAccess = new SqlDataAccess(new AssetContext());

        public async Task<IAsset> GetSingleAssetAsync(int dbId)
        {
            return await dataAccess.AssetDataAccess.ReadSingleIAsset(dbId);
        }

        public async Task<IAsset[]> GetAssetsAsync() => await dataAccess.Asset.ReadAllIAsset();

        public async Task DeleteAsset(IAsset asset) => await dataAccess.Asset.DeleteIAsset(asset);
    }
}
