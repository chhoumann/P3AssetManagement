using System.Threading.Tasks;
using AssetManagement.DataAccessLibrary;
using AssetManagement.Core;

namespace AssetManagement
{
    public class AssetService
    {
        private readonly SqlDataAccess dataAccess = new SqlDataAccess(new AssetContext());
        
        public async Task<IAsset[]> GetAssetsAsync() => await dataAccess.ReadAll();

        public async Task DeleteAsset(IAsset asset) => await dataAccess.Delete(asset);
    }
}
