using System;
using AssetManagement.Models.AssetHolder;

namespace AssetManagement.Models.Asset
{
    public sealed class AssetOwnershipHandler
    {
        private readonly Asset asset;

        public AssetOwnershipHandler(Asset asset)
        {
            this.asset = asset;
        }

        /// <summary>
        /// Transfer the asset to a new asset holder.
        /// </summary>
        /// <param name="newAssetHolder">The asset holder to transfer to.</param>
        private void TransferTo(IAssetHolder newAssetHolder)
        {
            // TODO:
            //  Transfer to new owner
            //  Invoke event (AssetRecord change)

            asset.AssetRecords.Add(new AssetRecord.AssetRecord(asset.LastAssetRecord.State, newAssetHolder, asset.AssetId));
            Console.WriteLine($"Added record for asset: {asset}");
        }

        public void ToDepot() => TransferTo(StaticAssetHolders.Depot);

        public void ToCage() => TransferTo(StaticAssetHolders.Cage);

        public void ToUser(IAssetHolder user) => TransferTo(user);
    }
}