using AssetManagement.Models.AssetHolder;
using AssetManagement.Models.AssetRecord;
using System;

namespace AssetManagement.Models.Asset
{
    public sealed class AssetOwnershipHandler
    {
        public event Action<Asset, IAssetHolder, AssetState> AssetOwnershipChanged;
        
        private readonly Asset asset;

        public AssetOwnershipHandler(Asset asset) => this.asset = asset;

        /// <summary>
        /// Transfer the asset to a new asset holder.
        /// </summary>
        /// <param name="newAssetHolder">The asset holder to transfer to.</param>
        private void TransferTo(IAssetHolder newAssetHolder)
        {
            AssetOwnershipChanged?.Invoke(asset, newAssetHolder, AssetState.Online);
        }

        public void ToDepot() => TransferTo(StaticAssetHolders.Depot);

        public void ToCage() => TransferTo(StaticAssetHolders.Cage);

        public void ToUser(IAssetHolder user) => TransferTo(user);
    }
}