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

        private void ChangeState(AssetState state)
        {
            if (Asset is Computer computer)
            {
                computer.ComputerRecords.Add(new ComputerRecord(computer, computer.CurrentAssetHolder, DateTime.Now, state));
            }
            AssetStateChanged?.Invoke();
        }

        public void ToOnline() => ChangeState(AssetState.Online);
        public void ToMissing() => ChangeState(AssetState.Missing);
        public void ToNull() => ChangeState(AssetState.Null);
    }
}