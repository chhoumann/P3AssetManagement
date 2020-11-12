using System.Threading.Tasks;
using System.Collections.Generic;
using AssetManagement.DataAccessLibrary;
using AssetManagement.DataAccessLibrary.Generic;
using AssetManagement.Models.Asset;

namespace AssetManagement.Server
{
    public class AssetService
    {
        private readonly SqlDataAccess<AssetData> genericDataAccess = new SqlDataAccess<AssetData>(new AssetContext());

        public async Task<Asset[]> GetAssetsAsync()
        {
            IEnumerable<AssetData> assetDatas = await genericDataAccess.GetAll();
            List<Asset> assets = new List<Asset>();

            // TODO:
            // Should also get the asset records for the asset and set up the asset model to contain its asset records.
            foreach (AssetData assetData in assetDatas)
            {
                assets.Add(new Asset(assetData.Id, assetData.Model, assetData.SerialNumber));
            }

            return assets.ToArray();
        }
    }
}
