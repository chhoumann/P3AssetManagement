using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;
using System;

namespace AssetManagement.DataAccessLibrary.DataModels.Handlers
{
    public class AssetStateHandler
    {
        public AssetStateHandler(IAsset asset) => Asset = asset;

        private IAsset Asset { get; }

        public static event Action AssetStateChanged;

        /// <summary>
        /// Simulates an state change of an asset by adding a new asset record to the asset with the new state.
        /// </summary>
        /// <param name="newState">The new state of the asset</param>
        private void ChangeState(AssetState newState)
        {
            if (newState == Asset.CurrentState)
            {
                return;
            }

            Asset.AddAssetRecord(Asset.CurrentHolder, DateTime.Now, newState);
            AssetStateChanged?.Invoke();
        }

        public void ToOnline() => ChangeState(AssetState.Online);
        public void ToMissing() => ChangeState(AssetState.Missing);
        public void ToNull() => ChangeState(AssetState.Null);
    }
}