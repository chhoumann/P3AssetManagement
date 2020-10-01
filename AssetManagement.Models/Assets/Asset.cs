using System;

namespace AssetManagement.Models
{
    public abstract class Asset
    {
        public virtual string Id { get; private set; }
        public virtual string Name { get; private set; }

        public bool IsDisposed { get; private set; }

        public DateTime LastSeen { get; private set; }

        // public Depot Depot { get; set; } = null;
        // public Employee Employee { get; set; } = null;

        public virtual void Transfer(AssetHolder assetHolder)
        {
            assetHolder.RecieveAsset(this);
        }

        public virtual void Dispose() { }
    }
}