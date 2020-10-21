using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.Models
{
    public static partial class AssetController
    {
        /// <summary>
        /// Transfer an asset to a new AssetHolder.
        /// </summary>
        /// <param name="assetToTransfer">The asset to transfer.</param>
        /// <param name="receiver">The receiver of the asset.</param>
        public static void TransferOwnership(IAsset assetToTransfer, AssetHolder receiver)
        {
            Asset asset = (Asset)assetToTransfer;
            asset.TransferTo(receiver);
        }

        /// <summary>
        /// Creates a new asset.
        /// </summary>
        public static IAsset MakeAsset(int id, string name, string serialNumber)
        {
            return new Asset(id, name, serialNumber);
        }

        /// <summary>
        /// Updates the PC-ID state of an asset and adds a new record to the asset.
        /// </summary>
        /// <param name="asset">The asset whose state needs to be updated.</param>
        /// <param name="assetState">The asset's new state.</param>
        public static void UpdateAssetState(IAsset asset, AssetState assetState)
        {
            asset.Records.Add(new StateRecord(assetState));
        }

        // Temporary, I hope. Early mockdata function, pls remove when proper mockdata is done
        public static Task<IAsset[]> GetAssetsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Employee anna = new Employee("Anna", "anna@acme.dk");
                Employee ulf = new Employee("Ulf", "ulf@acme.dk");

                List<Asset> assets = new List<Asset>()
                {
                    new Asset(69420, "Epic Dell Gaming PC", "SN1"),
                    new Asset(1234, "Not So Epic Dell Gaming PC", "SN1"),
                    new Asset(4, "HP PC", "SN1"),
                };

                TransferOwnership(assets[0], ulf);
                TransferOwnership(assets[1], ulf);
                TransferOwnership(assets[2], anna);

                return (IAsset[])assets.ToArray();
            });
        }
    }
}
