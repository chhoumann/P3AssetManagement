using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;

namespace AssetManagement.DataAccessLibrary.DataModels.Handlers
{
    public sealed class AssetOwnershipHandler
    {
        private IAsset Asset { get; }

        public AssetOwnershipHandler(IAsset asset) => Asset = asset;

        /// <summary>
        ///     An event which can be triggered when the status of an asset is updated (to sync with the database)
        /// </summary>
        public static event Action AssetOwnerShipChanged;

        /// <summary>
        /// Simulates an ownership transfer of an asset by adding a new asset record to the asset with the new holder.
        /// </summary>
        /// <param name="newHolder">The new holder of the asset.</param>
        private void TransferTo(AssetHolder newHolder)
        {
            if (Asset is Computer computer && newHolder != computer.CurrentHolder)
            {
                computer.ComputerRecords.Add(new ComputerRecord(computer, newHolder, DateTime.Now, AssetState.Online));
            }

            AssetOwnerShipChanged?.Invoke();
        }

        public void ToDepot() => TransferTo(ComputerService.Instance.Depot);

        public void ToCage() => TransferTo(ComputerService.Instance.Cage);

        public void ToUser(AssetHolder user) => TransferTo(user);
    }
}