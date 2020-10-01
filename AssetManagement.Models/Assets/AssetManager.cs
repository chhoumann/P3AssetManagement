namespace AssetManagement.Models
{
    public static class AssetManager
    {
        public static void TransferAsset(Asset asset, AssetHolder assetHolder)
        {
            // Update database here
            // ...

            // Transfer asset to new asset holder
            asset.TransferTo(assetHolder);
        }
    }
}