using System;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels.Interfaces;

namespace AssetManagement.DataAccessLibrary.DataModels.Handlers
{
    public class AssetStateHandler
    {
        public AssetStateHandler(IAsset asset) => Asset = asset;

        private IAsset Asset { get; }

        public static event Action<IAsset, AssetHolder, DateTime, AssetState> AssetStateChanged;

        private void ChangeState(AssetState state)
        {
            AssetStateChanged?.Invoke(Asset, Asset.CurrentAssetHolder, DateTime.Now, state);
        }

        public void Online() => ChangeState(AssetState.Online);
        public void Missing() => ChangeState(AssetState.Missing);
        public void Null() => ChangeState(AssetState.Null);
    }
}