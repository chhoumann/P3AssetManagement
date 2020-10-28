using System;

namespace AssetManagement.Models
{
    public sealed partial class AssetController
    {
        /// <summary>
        /// Transfer an asset to a new AssetHolder.
        /// </summary>
        /// <param name="assetToTransfer">The asset to transfer.</param>
        /// <param name="receiver">The receiver of the asset.</param>
        public static void TransferOwnership(IAssetData assetToTransfer, AssetHolder receiver)
        {
            if (assetToTransfer is Asset asset)
            {
                asset.TransferTo(receiver);
            }
            else
            {
                throw new ArgumentException("IAsset must be of type Asset!");
            }
        }

        /// <summary>
        /// Create a new asset.
        /// </summary>
        public static IAssetData MakeAsset(int id, string name, string serialNumber)
        {
            return new Asset(id, name, serialNumber);
        }

        /// <summary>
        /// Update the PC-ID state of an asset and add a new record to the asset.
        /// </summary>
        /// <param name="asset">The asset whose state needs to be updated.</param>
        /// <param name="assetState">The asset's new state.</param>
        public static void UpdateAssetState(IAssetData asset, AssetState assetState)
        {
            asset.AssetRecords.Add(new AssetRecord(assetState, asset.CurrentAssetHolder));
        }
    }
}
