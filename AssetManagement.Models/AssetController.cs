namespace AssetManagement.Models
{
    public sealed partial class AssetController
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
            asset.StateRecords.Add(new StateRecord(assetState));
        }
    }
}
