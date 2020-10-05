namespace AssetManagement.Models
{
    public enum AssetState { Online, Offline, Disposed }

    public abstract class Asset
    {
        public virtual string Name { get; private set; }

        protected AssetState state;
        public virtual AssetState State 
        { 
            get => state;
            set => state = value;
        }

        public AssetHolder CurrentAssetHolder { get; private set; }

        public Asset(string name) => Name = name;

        public void TransferTo(AssetHolder newAssetHolder)
        {
            // Remove this asset from its current holder, if any
            if (CurrentAssetHolder != null)
            {
                CurrentAssetHolder.RemoveAsset(this);
            }

            // Update the current holder by transferring this asset to the new AssetHolder
            newAssetHolder.RecieveAsset(this);
            CurrentAssetHolder = newAssetHolder;
        }

        public void Dispose() { }
    }
}