using AssetManagement.Core;
using System;

namespace AssetManagement.Models
{
    /// <summary>
    /// Factory controller responsible for creating assets, transfering assets and updating asset states.
    /// </summary>
    public sealed partial class AssetController
    {
        /// <summary>
        /// Transfer an asset to a new AssetHolder.
        /// </summary>
        /// <param name="assetToTransfer">The asset to transfer.</param>
        /// <param name="receiver">The receiver of the asset.</param>
        public static void TransferOwnership(IAsset assetToTransfer, IAssetHolder receiver)
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
        /// Creates a new asset and returns it as an IAsset interface.
        /// </summary>
        /// <param name="id">The asset id.</param>
        /// <param name="model">The name of the asset model.</param>
        /// <param name="serialNumber">The asset's serial number</param>
        /// <returns></returns>
        public static IAsset MakeAsset(int id, string model, string serialNumber)
        {
            return new Asset(id, model, serialNumber);
        }

        /// <summary>
        /// Update the PC-ID state of an asset and add a new record to the asset.
        /// </summary>
        /// <param name="asset">The asset whose state needs to be updated.</param>
        /// <param name="assetState">The asset's new state.</param>
        public static void UpdateAssetState(IAsset asset, AssetState assetState)
        {
            asset.AssetRecords.Add(new AssetRecord(assetState, asset.CurrentAssetHolder));
        }
    }
}
