using System;

namespace AssetManagement.Models
{
    public enum OnlineAssetState { Online, Offline }

    public sealed class OnlineAsset : Asset
    {
        public OnlineAssetState State { get; private set; }

        public DateTime LastSeen { get; private set; }
    }
}