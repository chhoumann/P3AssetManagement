using System.Threading.Tasks;
using AssetManagement.Models;
using AssetManagement.Core;

namespace AssetManagement
{
    public class AssetService
    {
        public Task<IAsset[]> GetAssetsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Employee anna = new Employee("Anna", "anna@acme.dk", "AAF");
                Employee ulf = new Employee("Ulf", "ulf@acme.dk", "AAF");
                Depot depot = new Depot();

                IAsset[] assets = new IAsset[]
                {
                    AssetController.MakeAsset(69420, "Epic Dell Gaming PC", "SN1"),
                    AssetController.MakeAsset(1234, "Not So Epic Dell Gaming PC", "SN1"),
                    AssetController.MakeAsset(4, "HP PC", "SN1"),
                    AssetController.MakeAsset(5, "Jacob's ThinkPad", "SN1"),
                };

                AssetController.TransferOwnership(assets[0], ulf);
                AssetController.TransferOwnership(assets[1], ulf);
                AssetController.TransferOwnership(assets[2], anna);
                AssetController.TransferOwnership(assets[3], depot);

                return assets;
            });
        }
    }
}
