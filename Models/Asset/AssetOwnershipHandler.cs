using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;
using System;

namespace AssetManagement.Models.Asset
{
    public sealed class AssetOwnershipHandler
    {
        /// <summary>
        /// An event which can be triggered when the status of an asset is updated somehow
        /// </summary>
        public static event Action<Asset, IAssetHolder, AssetState> AssetStatusChanged;

        private readonly Asset asset;

        public AssetOwnershipHandler(Asset asset) => this.asset = asset;

        /// <summary>
        /// Transfer the asset to a new asset holder.
        /// </summary>
        /// <param name="newAssetHolder">The asset holder to transfer to.</param>
        private void TransferTo(IAssetHolder newAssetHolder)
        {
            AssetStatusChanged?.Invoke(asset, newAssetHolder, AssetState.Online);
        }

        public void ToDepot() => TransferTo(StaticAssetHolders.Depot);

        public void ToCage() => TransferTo(StaticAssetHolders.Cage);

        public void ToUser(IAssetHolder user) => TransferTo(user);
    }
}