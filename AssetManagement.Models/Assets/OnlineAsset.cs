using System;

namespace AssetManagement.Models
{
    public sealed class OnlineAsset : Asset
    {
        public string Id { get; private set; }
        public DateTime LastSeen { get; private set; }

        public OnlineAsset(string id, string name) : base(name)
        {
            Id = id;
            state = AssetState.Online;
        }
    }
}