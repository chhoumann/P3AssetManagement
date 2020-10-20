using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManagement.Models;

namespace AssetManagement
{
    public class AssetService
    {
        public Task<Asset[]> GetAssetsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Employee anna = new Employee("Anna", "anna@acme.dk");

                List<Asset> assets = new List<Asset>()
                {
                    new Asset(69420, "Epic Dell Gaming PC", "SN1", anna),
                    new Asset(1234, "Not So Epic Dell Gaming PC", "SN1", anna),
                    new Asset(4, "HP PC", "SN1", anna),
                };

                Employee ulf = new Employee("Ulf", "ulf@acme.dk");

                assets[0].TransferTo(ulf);
                assets[1].TransferTo(ulf);

                return assets.ToArray();
            });
        }
    }
}
