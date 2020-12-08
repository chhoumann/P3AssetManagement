using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;

namespace AssetManagement.DataAccessLibrary.DataModels.Handlers
{
    public sealed class AssetOwnershipHandler
    {
        private IAsset Asset { get; }

        public AssetOwnershipHandler(IAsset asset) => this.Asset = asset;

        /// <summary>
        ///     An event which can be triggered when the status of an asset is updated to sync with the database
        /// </summary>
        public static event Action AssetOwnerShipChanged;

        private void TransferTo(AssetHolder newHolder)
        {
            if (Asset is Computer computer)
            {
                computer.ComputerRecords.Add(new ComputerRecord(computer, newHolder, DateTime.Now, AssetState.Online));
            }
            AssetOwnerShipChanged?.Invoke();
        }

        public void ToDepot() => TransferTo(new ComputerService().Depot);

        public void ToCage() => TransferTo(new ComputerService().Cage);

        public void ToUser(AssetHolder user) => TransferTo(user);
    }
}