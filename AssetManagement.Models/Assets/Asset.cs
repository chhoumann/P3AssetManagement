namespace AssetManagement.Models
{
    public abstract class Asset
    {
        public virtual string Id { get; private set; }
        public virtual string Name { get; private set; }

        public bool IsDisposed { get; private set; }

        private AssetHolder currentHolder;

        public virtual void TransferTo(AssetHolder assetHolder)
        {
            // Remove this asset from its current holder, if any
            if (currentHolder != null)
            {
                currentHolder.RemoveAsset(this);
            }

            // Update the current holder by transferring this asset to the new AssetHolder
            currentHolder = assetHolder.RecieveAsset(this);
        }

        public virtual void Dispose() { }
    }
}