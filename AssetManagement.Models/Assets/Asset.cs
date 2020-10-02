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

        public AssetHolder CurrentHolder { get; private set; }

        public Asset(string name) => Name = name;

        public void TransferTo(AssetHolder assetHolder)
        {
            // Remove this asset from its current holder, if any
            if (CurrentHolder != null)
            {
                CurrentHolder.RemoveAsset(this);
            }

            // Update the current holder by transferring this asset to the new AssetHolder
            CurrentHolder = assetHolder.RecieveAsset(this);
        }

        public void Dispose() { }
    }
}