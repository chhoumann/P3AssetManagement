using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;

namespace AssetManagement.DataAccessLibrary.DataModels.Handlers
{
    public sealed class AssetOwnershipHandler<TAsset, TAssetService> : IAssetOwnershipHandler
        where TAsset : IAsset
        where TAssetService : IAssetService<TAsset>, new()
    {
        private IAsset Asset { get; }

        public AssetOwnershipHandler(IAsset asset) => Asset = asset;

        /// <summary>
        ///     An event which can be triggered when the status of an asset is updated (to sync with the database)
        /// </summary>
        public static event Action AssetOwnershipChanged;

        /// <summary>
        /// Simulates an ownership transfer of an asset by adding a new asset record to the asset with the new holder.
        /// </summary>
        /// <param name="newHolder">The new holder of the asset.</param>
        private void TransferTo(AssetHolder newHolder)
        {
            if (Asset is Computer computer) // TODO: Make this part generic
            {
                computer.ComputerRecords.Add(new ComputerRecord(computer, newHolder, DateTime.Now, AssetState.Online));
            }

            AssetOwnershipChanged?.Invoke();
        }

        public void ToDepot() => TransferTo(new TAssetService().Depot);

        public void ToCage() => TransferTo(new TAssetService().Cage);

        public void ToUser(AssetHolder user) => TransferTo(user);
    }
}