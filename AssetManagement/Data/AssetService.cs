using System.Threading.Tasks;
using AssetManagement.Models;

namespace AssetManagement
{
    public class AssetService
    {
        public Task<IAsset[]> GetAssetsAsync()
        {
            return AssetController.GetAssetsAsync();
        }
    }
}
