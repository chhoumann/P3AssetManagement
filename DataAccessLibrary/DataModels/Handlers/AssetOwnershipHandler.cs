using System;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.DataAccessLibrary.DataModels
{
    public sealed class AssetOwnershipHandler
    {
        private readonly IAsset asset;

        public AssetOwnershipHandler(IAsset asset) => this.asset = asset;

        /// <summary>
        ///     An event which can be triggered when the status of an asset is updated to sync with the database
        /// </summary>
        public static event Action<IAsset, string, DateTime, AssetState> AssetHolderChanged;

        private void TransferTo(string newHolderUsername)
        {
            AssetHolderChanged?.Invoke(asset, newHolderUsername, DateTime.Now, AssetState.Online);
        }

        public void ToDepot() => TransferTo(StaticUsernames.Depot);

        public void ToCage() => TransferTo(StaticUsernames.Cage);

        public void ToUser(AssetHolder user) => TransferTo(user.Username);
    }
}